using Car_Rental_Management.Dtos;
using Car_Rental_Management.Mapper;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Implement;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ICustomerService _customerService;
        private readonly ICarRepository _carRepo;
        private readonly IDriverRepository _driverRepo;

        public BookingController(
            IBookingService bookingService,
            ICustomerService customerService,
            ICarRepository carRepo,
            IDriverRepository driverRepo)
        {
            _bookingService = bookingService;
            _customerService = customerService;
            _carRepo = carRepo;
            _driverRepo = driverRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid id)   // id = CarId
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Customer");

            var customerGuid = Guid.Parse(userId);
            var customer = await _customerService.GetByIdAsync(customerGuid);
            if (customer == null)
                return RedirectToAction("Register", "Customer");

            // Car check
            var car = await _carRepo.GetByIdAsync(id);
            if (car == null)
                return RedirectToAction("Index", "Home");

            var model = new BookingViewmodel
            {
                CustomerId = customerGuid,
                CarId = car.Id,
                CarMake = car.Make,
                CarModel = car.Model,
                CarColor = car.Color,
                CarPricePerDay = car.PricePerDay ?? 0,
                CarImage = car.ImagePaths?.FirstOrDefault() ?? "/uploads/images/noimage.jpg",
                //Drivers = await _driverRepo.GetAllAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewmodel model)
        {
            if (!ModelState.IsValid) return View(model);

            int totalDays = (model.EndDate - model.StartDate).Days + 1;
            decimal driverFee = 0;

            if (model.BookingType == "WithDriver" && model.DriverId.HasValue)
            {
                var driver = (await _driverRepo.GetAllAsync()).FirstOrDefault(d => d.Id == model.DriverId.Value);
                //if (driver != null)
                //    driverFee = driver.FeePerDay;
            }

            model.TotalPrice = (model.CarPricePerDay + driverFee) * totalDays;

            try
            {
                await _bookingService.CreateBookingAsync(model);
                TempData["Message"] = "Booking confirmed!";
                return RedirectToAction("Payment", "Payment");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


        [HttpGet]
        public async Task<IActionResult> AllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return View(bookings);
        }


        [HttpGet]
        public async Task<IActionResult> MyBookings(Guid customerId)
        {
            var bookings = await _bookingService.GetBookingsByCustomerIdAsync(customerId);
            return View(bookings);
        }
        [HttpGet]
        public async Task<IActionResult> Booking()
        {
            var cars = await _carRepo.GetAllAsync();
            var drivers = await _driverRepo.GetAllAsync();

            var model = new BookingFormViewModel
            {
                Cars = CarMapper.ToDtoList(cars),
                Drivers = drivers.Select(d => new DriverDto
                {
                    Id = d.Id,
                    Name = d.Name
                    // Fee ignore for now
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Booking(BookingFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // reload cars & drivers
                var cars = await _carRepo.GetAllAsync();
                var drivers = await _driverRepo.GetAllAsync();

                model.Cars = CarMapper.ToDtoList(cars);
                model.Drivers = drivers.Select(d => new DriverDto
                {
                    Id = d.Id,
                    Name = d.Name
                }).ToList();

                return View(model);
            }

            int totalDays = (model.EndDate - model.StartDate).Days + 1;
            if (totalDays <= 0)
            {
                ModelState.AddModelError("", "End Date must be after Start Date.");
                var cars = await _carRepo.GetAllAsync();
                var drivers = await _driverRepo.GetAllAsync();

                model.Cars = CarMapper.ToDtoList(cars);
                model.Drivers = drivers.Select(d => new DriverDto
                {
                    Id = d.Id,
                    Name = d.Name
                }).ToList();

                return View(model);
            }

            var selectedCar = (await _carRepo.GetAllAsync()).FirstOrDefault(c => c.Id == model.CarId);
            if (selectedCar == null)
            {
                ModelState.AddModelError("", "Selected car not found.");
                var cars = await _carRepo.GetAllAsync();
                var drivers = await _driverRepo.GetAllAsync();

                model.Cars = CarMapper.ToDtoList(cars);
                model.Drivers = drivers.Select(d => new DriverDto
                {
                    Id = d.Id,
                    Name = d.Name
                }).ToList();

                return View(model);
            }

            decimal carPrice = selectedCar.PricePerDay ?? 0;

            model.TotalPrice = carPrice * totalDays; // driver fee ignore

            try
            {
                // Map BookingFormViewModel -> BookingViewmodel
                var booking = new BookingViewmodel
                {
                    CarId = model.CarId,
                    DriverId = model.DriverId,
                    BookingType = model.BookingType,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    TotalPrice = model.TotalPrice
                };

                await _bookingService.CreateBookingAsync(booking);

                TempData["Message"] = "Booking confirmed!";
                return RedirectToAction("Payment", "Payment", new { bookingId = model.CarId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                var cars = await _carRepo.GetAllAsync();
                var drivers = await _driverRepo.GetAllAsync();
                model.Cars = CarMapper.ToDtoList(cars);
                model.Drivers = drivers.Select(d => new DriverDto
                {
                    Id = d.Id,
                    Name = d.Name
                }).ToList();

                return View(model);
            }
        }

    }
}

