using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;
using System.Threading.Tasks;

namespace Car_Rental_Management.Service.Interface
{
    public interface IDriverService
    {
        Task<string> CreateDriverAsync(DriverViewModel viewModel);
        Task<List<DriverDto>> GetAllDriversAsync();
        Task<DriverDto?> GetDriverByIdAsync(Guid id);
        Task<string> UpdateDriverAsync(DriverViewModel viewModel);
        Task<string> DeleteDriverAsync(Guid id);
    }
}
