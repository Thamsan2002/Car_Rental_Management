using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IDriverRepository
    {
        Task<Driver?> GetByIdAsync(Guid id);
        Task<IEnumerable<Driver>> GetAllAsync();
        Task<Driver> AddAsync(Driver driver);
        Task<Driver> UpdateAsync(Driver driver);
        Task DeleteAsync(Driver driver);
    }
}
