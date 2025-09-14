using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Interface
{
    public interface ICustomerService
    {
        Task<string> CreateCustomerAsync(CustomerViewModel viewModel);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
    }
}
