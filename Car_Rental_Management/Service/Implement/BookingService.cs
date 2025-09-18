using Car_Rental_Management.Dtos;
using Car_Rental_Management.Mapper;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Implement;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Implement
{
    public class BookingService:IBookingService
    {
        private readonly IBookingRepository _repo;
        private readonly ICarRepository _carRepo;
        private readonly IDriverRepository _driverRepo;

        public BookingService(IBookingRepository repo, ICarRepository carRepo, IDriverRepository driverRepo)
        {
            _repo = repo;
            _carRepo = carRepo;
            _driverRepo = driverRepo;
        }


        public async Task<List<BookingDateRangeDto>> GetCarBookingDatesAsync(Guid carId)
        {
            return await _repo.GetBookingDateRangesByCarAsync(carId);
        }

        public async Task<Guid> CreateBookingAsync(BookingViewmodel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Booking model cannot be null.");

            try
            {
                // Map ViewModel to Booking entity
                var booking = new Booking
                {
                    CustomerId = model.CustomerId,
                    CarId = model.CarId,
                    DriverId = model.DriverId, // nullable
                    BookingType = model.BookingType,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    TotalPrice = model.TotalPrice,
                    CreatedAt = DateTime.Now
                };

                // Save to database via repository
                await _repo.CreateAsync(booking);

                return booking.Id;
            }
            catch (Exception ex)
            {
                // Optional: log the exception here
                throw new Exception("Failed to create booking. Please try again.", ex);
            }
        }


        //public async Task<BookingViewmodel> CreateBookingAsync(BookingViewmodel dto)
        //{
        //    if (dto.CustomerId == Guid.Empty) throw new Exception("Customer ID missing");
        //    if (dto.CarId == Guid.Empty) throw new Exception("Car ID missing");
        //    if (dto.StartDate >= dto.EndDate) throw new Exception("Invalid date range");

        //    var model = BookingMapper.ToModel(dto);
        //    await _repo.CreateAsync(model);

        //    // map back to viewmodel including generated Booking Id
        //    var result = BookingMapper.ToDto(model);
        //    result.BookingId = model.Id; // important: set Id for redirect
        //    return result;
        //}

        //public async Task<(bool Success, string Message, Guid BookingId)> CreateBookingAsync(BookingViewmodel model)
        //{
        //    // 1️⃣ Map ViewModel → Booking entity
        //    var booking = new Booking
        //    {
        //        CustomerId = model.CustomerId,
        //        CarId = model.CarId,
        //        BookingType = model.BookingType,
        //        DriverId = model.DriverId,
        //        StartDate = model.StartDate,
        //        EndDate = model.EndDate,
        //        TotalPrice = model.TotalPrice,
        //        CreatedAt = DateTime.Now
        //    };

        //    // 2️⃣ Save booking
        //    await _repo.AddAsync(booking);

        //    // 3️⃣ Update Car availability
        //    var car = await _carRepo.GetByIdAsync(model.CarId);
        //    if (car != null)
        //    {
        //        car.IsAvailable = false;
        //        await _carRepo.UpdateAsync(car);
        //    }

        //    return (true, "Booking successful", booking.Id);
        //}

        //public async Task<BookingViewmodel> GetBookingDtoByIdAsync(Guid id)
        //{
        //    var booking = await _repo.GetByIdAsync(id);
        //    if (booking == null) return null;

        //    var dto = BookingMapper.ToDto(booking);

        //    var car = await _carRepo.GetByIdAsync(booking.CarId);
        //    if (car != null)
        //    {
        //        dto.CarMake = car.Make;
        //        dto.CarModel = car.Model;
        //        dto.CarColor = car.Color;
        //        dto.CarPricePerDay = car.PricePerDay ?? 0;
        //        dto.CarImage = car.ImagePaths?.FirstOrDefault() ?? "/uploads/images/noimage.jpg";
        //    }

        //    return dto;
        //}

        //public async Task<List<BookingViewmodel>> GetAllBookingsAsync()
        //{
        //    var bookings = await _repo.GetAllAsync();
        //    return bookings.Select(BookingMapper.ToDto).ToList();
        //}

        //public async Task<List<BookingViewmodel>> GetBookingsByCustomerIdAsync(Guid customerId)
        //{
        //    var bookings = await _repo.GetByCustomerIdAsync(customerId);
        //    return bookings.Select(BookingMapper.ToDto).ToList();
        //}
    }
}
