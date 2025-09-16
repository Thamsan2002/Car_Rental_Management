using Car_Rental_Management.ViewModel;
using Car_Rental_Management.Dtos;
using System.Threading.Tasks;

namespace Car_Rental_Management.Service.Interface
{
    public interface ICarService
    {
        Task<bool> AddCarAsync(CarViewModel model);
        Task<List<CarDto>> GetAllCarsAsync();
        Task<CarDto?> GetCarByIdAsync(Guid id);
        Task<bool> UpdateCarAsync(CarViewModel model);
        Task<bool> DeleteCarAsync(Guid id);

    }
}
