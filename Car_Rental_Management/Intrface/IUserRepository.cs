using Car_Rental_Management.Models;

namespace Car_Rental_Management.Intrface
{
    public interface IUserRepository
    {
        Task<Guid> AddAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);


    }
}
