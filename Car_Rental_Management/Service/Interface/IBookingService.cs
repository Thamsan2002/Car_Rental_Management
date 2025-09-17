using Car_Rental_Management.Dtos;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Interface
{
    public interface IBookingService
    {
        Task CreateBookingAsync(BookingViewmodel dto);
        Task<BookingViewmodel> GetBookingDtoByIdAsync(Guid id);
        Task<List<BookingViewmodel>> GetAllBookingsAsync();
        Task<List<BookingViewmodel>> GetBookingsByCustomerIdAsync(Guid customerId);
    }
}
