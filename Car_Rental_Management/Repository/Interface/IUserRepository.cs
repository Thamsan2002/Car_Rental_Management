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

        Task<User?> GetByEmailOrPhoneAsync(string emailOrPhone, string password);
        User? GetById(Guid userId);

        // Update user details
        void Update(User user);
       


        Task<User?> GetByRoleAsync(string role);
      
    }
}
