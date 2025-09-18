using Car_Rental_Management.Models;
using System;
using System.Threading.Tasks;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IUserRepository
    {
        Task<Guid> AddAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task UpdateAsync(User user);
        Task<bool> IsEmailOrPhoneExistAsync(string email, string phone);
        Task<User?> GetByPhoneAsync(string phoneNumber);

        // 🔹 Add this method in interface
        Task<User?> GetByEmailOrPhoneAsync(string emailOrPhone);

        Task<User?> GetCustomerByLoginAsync(string emailOrPhone);

        Task<User?> GetByRoleAsync(string role);
      
    }
}
