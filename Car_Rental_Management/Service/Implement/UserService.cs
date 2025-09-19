using Car_Rental_Management.Mapper;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using System;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Car_Rental_Management.Service.Implement
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found");
            return user;
        }

        public async Task<bool> VerifyPasswordAsync(Guid userId, string password)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return false;

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }

        public async Task UpdateProfileAsync(Guid userId, UserViewModel vm)
        {
            var existingUser = await _userRepo.GetByIdAsync(userId);
            if (existingUser == null) throw new Exception("User not found");

            var updatedUser = UserMapper.ToModel(vm, userId);

            // Preserve existing password hash
            updatedUser.PasswordHash = existingUser.PasswordHash;

            await _userRepo.UpdateAsync(updatedUser);
        }

        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, string confirmPassword)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                throw new Exception("Current password is incorrect");

            if (newPassword != confirmPassword)
                throw new Exception("New password and confirm password do not match");

            // Hash new password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

            await _userRepo.UpdateAsync(user);
        }
    }
}
