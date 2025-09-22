using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IBookingRepository
    {
        Task<List<BookingDateRangeDto>> GetBookingDateRangesByCarAsync(Guid carId);
        Task CreateAsync(Booking booking);
     
        Task<List<Booking>> GetConfirmedBookingsByCustomerAsync(Guid customerId);
        Task<Booking?> GetByIdAsync(Guid bookingId);


        // NEW → Get All Bookings
        Task<List<Booking>> GetAllBookingsAsync();

        Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(Guid customerId);
    }
}
