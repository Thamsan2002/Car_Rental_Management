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
                    Status = "Active",
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

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _repo.GetAllBookingsAsync();
        }

        public async Task<Dictionary<int, int>> GetHourlyBookingStatsAsync()
        {
            var bookings = await _repo.GetAllBookingsAsync();
            var hourlyCount = new Dictionary<int, int>();

            foreach (var booking in bookings)
            {
                int hour = booking.CreatedAt.Hour;
                if (!hourlyCount.ContainsKey(hour))
                    hourlyCount[hour] = 0;

                hourlyCount[hour]++;
            }

            return hourlyCount; // Ex: {9: 5 bookings, 14: 12 bookings}
        }

        public async Task<Booking?> GetActiveBookingAsync(Guid customerId)
        {
            // Booking table-la active booking edukkanum
            var bookings = await _repo.GetBookingsByCustomerIdAsync(customerId);

            // active booking = finish aagatha booking
            return bookings.FirstOrDefault(b => b.Status != "Finished");
        }

    }
}
