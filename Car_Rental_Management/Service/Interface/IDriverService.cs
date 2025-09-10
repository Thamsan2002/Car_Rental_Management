using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Interface
{
    public interface IDriverService
    {
        Task<IEnumerable<DriverDto>> GetAllDriversAsync();
        Task<DriverDto?> GetDriverByIdAsync(Guid id);
        Task<DriverDto> CreateDriverAsync(DriverViewModel vm);
        Task<DriverDto> UpdateDriverAsync(DriverViewModel vm);
        Task DeleteDriverAsync(Guid id);
    }
}
