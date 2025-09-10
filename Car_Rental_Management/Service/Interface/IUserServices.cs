using Car_Rental_Management.Models;

namespace Car_Rental_Management.Service.Interface
{
    public interface IUserServices
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
    }
}
