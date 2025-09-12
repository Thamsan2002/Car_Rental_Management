using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface ICarRepository
    {
        Task AddAsync(Car car);
    }
}
