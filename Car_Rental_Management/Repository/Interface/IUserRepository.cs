using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IUserRepository
    {
        Task<Guid> AddAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByEmailAndPhoneAsync(string email, string phone);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
