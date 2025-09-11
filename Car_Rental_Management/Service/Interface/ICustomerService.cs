using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Interface
{
    public interface ICustomerService
    {
        Task<CustomerDto> AddCustomerAsync(CustomerViewModel vm);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerViewModel?> GetCustomerByIdAsync(Guid id);
        Task<CustomerDto> UpdateCustomerAsync(CustomerViewModel vm);
        Task DeleteCustomerAsync(Guid id);
    }
}
