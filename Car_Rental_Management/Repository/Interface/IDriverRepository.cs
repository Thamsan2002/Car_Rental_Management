using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IDriverRepository
    {
        Task<Driver> AddAsync(Driver driver);
        Task<Driver?> GetByIdAsync(Guid id);
        Task<List<Driver>> GetAllAsync();
        Task UpdateAsync(Driver driver);
        Task DeleteAsync(Driver driver);
        Task<int> GetDriverCountAsync();
        Task<Driver?> GetRandomDriverAsync(Guid? excludeId = null);

    }
}
