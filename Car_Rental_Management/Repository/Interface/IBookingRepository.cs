using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IBookingRepository
    {
        Task<List<BookingDateRangeDto>> GetBookingDateRangesByCarAsync(Guid carId);
        Task CreateAsync(Booking booking);
        //Task AddAsync(Booking booking);
        //Task<List<Booking>> GetAllAsync();
        //Task<Booking> GetByIdAsync(Guid id);
        //Task<List<Booking>> GetByCustomerIdAsync(Guid customerId);
    }
}
