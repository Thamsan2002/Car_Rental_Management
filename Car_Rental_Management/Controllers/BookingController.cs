using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Car_Rental_Management.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ICustomerService _customerService;
        private readonly ICarRepository _carRepo;
        private readonly IDriverRepository driverRepository;


        public BookingController(
            IBookingService bookingService,
            ICustomerService customerService,
            ICarRepository carRepo,
            IDriverRepository driverRepository)

        {
            _bookingService = bookingService;
            _customerService = customerService;
            _carRepo = carRepo;
            this.driverRepository = driverRepository;

        }

        // ✅ Create Booking GET
        [HttpGet]
        public async Task<IActionResult> Create(Guid id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToAction("Login", "Customer");

            var customerGuid = Guid.Parse(userId);
            var customer = await _customerService.GetByIdAsync(customerGuid);
            if (customer == null)
                return RedirectToAction("Register", "Customer");

            var car = await _carRepo.GetByIdAsync(id);
            if (car == null)
                return RedirectToAction("Index", "Home");

            var randomDriver = await driverRepository.GetRandomDriverAsync();
            var bookingDates = await _bookingService.GetCarBookingDatesAsync(id);
            var model = new BookingViewmodel
            {
                BookedPeriods = bookingDates,
                CustomerId = customer.Id,
                DriverId = randomDriver?.Id,
                DriverName = randomDriver?.Name,
                CarId = car.Id,
                CarMake = car.Make,
                CarModel = car.Model,
                CarColor = car.Color,
                CarPricePerDay = car.PricePerDay ?? 0,
                CarImage = car.ImagePaths?.FirstOrDefault() ?? "/uploads/images/noimage.jpg",

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewmodel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Call service to create booking
                var bookingId = await _bookingService.CreateBookingAsync(model);

                // Redirect to payment page
                return RedirectToAction("Create", "Driver", new { bookingId });
            }
            catch (ArgumentNullException argEx)
            {
                ModelState.AddModelError("", argEx.Message);
            }
            catch (InvalidOperationException dbEx)
            {
                ModelState.AddModelError("", dbEx.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred while creating your booking."+ex);
                // Optional: log ex here
                Console.WriteLine(ex);
            }

            return View(model); // return view with error messages
        }


        //// ✅ All Bookings (for admin)
        //[HttpGet]
        //public async Task<IActionResult> AllBookings()
        //{
        //    var bookings = await _bookingService.GetAllBookingsAsync();

        //    var monthlyData = bookings
        //        .GroupBy(b => new { b.StartDate.Year, b.StartDate.Month })
        //        .Select(g => new
        //        {
        //            Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM yyyy", CultureInfo.InvariantCulture),
        //            Count = g.Count()
        //        }).ToList();

        //    var carData = bookings
        //        .GroupBy(b => $"{b.CarMake} {b.CarModel}")
        //        .Select(g => new
        //        {
        //            Car = g.Key,
        //            Count = g.Count()
        //        }).ToList();

        //    ViewBag.MonthlyData = monthlyData;
        //    ViewBag.CarWiseData = carData;

        //    return View(bookings);
        //}

        //// ✅ My Bookings (for customer)
        //[HttpGet]
        //public async Task<IActionResult> MyBookings(Guid customerId)
        //{
        //    var bookings = await _bookingService.GetBookingsByCustomerIdAsync(customerId);
        //    return View(bookings);
        //}

    }
}
