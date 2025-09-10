using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> AddAsync(Customer customer);
        Task<Customer> UpdateAsync(Customer customer);
        Task DeleteAsync(Customer customer);
    }
}
