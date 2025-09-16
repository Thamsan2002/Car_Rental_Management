using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Interface
{
    public interface ICustomerService
    {
        Task<(bool isSuccess, string errorMessage, Guid? customerId)> RegisterCustomerAsync(CustomerRegisterViewModel model);
        Task<User?> LoginCustomerAsync(CustomerLoginViewModel model);
        Task<String> RegisterUserAsync(CustomerSignupViewmodel model);
        Task<Customer> GetByIdAsync(Guid customerId);
        Task<CustomerRegisterViewModel?> GetCustomerByUserIdAsync(Guid userId);

    }
}
