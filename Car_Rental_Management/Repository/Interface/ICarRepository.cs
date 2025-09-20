using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface ICarRepository
    {
        Task AddAsync(Car car);
        Task<List<Car>> GetAllAsync();
        Task<Car?> GetByIdAsync(Guid id);
        Task UpdateAsync(Car car);
        Task DeleteAsync(Car car);
        Task<int> GetCarCountAsync();
        Task<int> GetAvailableCarCountAsync();
        Task<Car> GetCarByIdAsync(Guid id);
        Task<List<Car>> GetAvailableCarsAsync();
        Task UpdateCarAsync(Car car);
      
    }
}
