using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<Customer> AddAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllAsync();
    }
}
