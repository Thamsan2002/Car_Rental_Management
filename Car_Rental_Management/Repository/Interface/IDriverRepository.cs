using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IDriverRepository
    {
        Task<Driver> AddAsync(Driver driver);
        Task<IEnumerable<Driver>> GetAllAsync();
      
    }
}
