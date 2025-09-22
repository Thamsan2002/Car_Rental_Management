using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Interface
{
    public interface IBookingService
    {
        Task<List<BookingDateRangeDto>> GetCarBookingDatesAsync(Guid carId);
        Task<Guid> CreateBookingAsync(BookingViewmodel model);
       
        // NEW
        Task<List<Booking>> GetAllBookingsAsync();
        Task<Dictionary<int, int>> GetHourlyBookingStatsAsync();

         Task<Booking?> GetActiveBookingAsync(Guid customerId);



    }
}
