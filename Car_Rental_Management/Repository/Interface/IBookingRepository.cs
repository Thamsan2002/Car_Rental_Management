using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IBookingRepository
    {
        Task<Booking> GetByIdAsync(Guid id);
        Task<List<Booking>> GetAllAsync();
        Task CreateAsync(Booking booking);
    }
}
