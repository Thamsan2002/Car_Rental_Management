using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;
using System.Threading.Tasks;

namespace Car_Rental_Management.Service.Interface
{
    public interface IDriverService
    {
        Task<string> CreateDriverAsync(DriverViewModel viewModel); // Password hashing handled in implementation
        Task<List<DriverDto>> GetAllDriversAsync();
        Task<DriverDto?> GetDriverByIdAsync(Guid id);
        Task<string> UpdateDriverAsync(DriverViewModel viewModel); // Password hashed if updated
        Task<string> DeleteDriverAsync(Guid id);
    }
}
