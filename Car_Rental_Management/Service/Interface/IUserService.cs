using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;
using System;
using System.Threading.Tasks;

namespace Car_Rental_Management.Service.Interface
{
    public interface IUserService
    {
        // Fetch user by Id
        Task<User> GetUserByIdAsync(Guid userId);

        // Verify password
        Task<bool> VerifyPasswordAsync(Guid userId, string password);

        // Update profile (without changing password)
        Task UpdateProfileAsync(Guid userId, UserViewModel vm);

        // Change password with hashing
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, string confirmPassword);
    }
}
