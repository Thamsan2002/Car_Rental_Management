using Car_Rental_Management.Dtos;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Interface
{
    public interface IBookingService
    {
        Task<List<BookingDateRangeDto>> GetCarBookingDatesAsync(Guid carId);
        Task<Guid> CreateBookingAsync(BookingViewmodel model);
        //Task<(bool Success, string Message, Guid BookingId)> CreateBookingAsync(BookingViewmodel model);
        //Task<BookingViewmodel> GetBookingDtoByIdAsync(Guid id);
        //Task<List<BookingViewmodel>> GetAllBookingsAsync();
        //Task<List<BookingViewmodel>> GetBookingsByCustomerIdAsync(Guid customerId);

    }
}
