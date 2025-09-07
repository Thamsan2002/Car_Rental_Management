using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Interface
{
    public interface IDriverService
    {
        Task<string> CreateDriverAsync(DriverViewModel viewModel);
        Task<IEnumerable<Driver>> GetAllDriversAsync();

    }
}
