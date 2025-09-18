using Car_Rental_Management.Mapper;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Implement
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public User GetUserById(Guid userId)
        {
            return _userRepo.GetById(userId);
        }

        public bool VerifyPassword(Guid userId, string password)
        {
            var user = _userRepo.GetById(userId);
            if (user == null) return false;

            return user.Password == password; 
        }

        public void UpdateProfile(Guid userId, UserViewModel vm)
        {
            // 1️⃣ Fetch existing user
            var existingUser = _userRepo.GetById(userId);
            if (existingUser == null)
                throw new Exception("User not found");

            // 2️⃣ Use Mapper to map only profile fields (exclude password)
            var updatedUser = UserMapper.ToModel(vm, userId);

            // 3️⃣ Preserve existing password
            updatedUser.Password = existingUser.Password;

            // 4️⃣ Save changes
            _userRepo.Update(updatedUser);
        }

        public void ChangePassword(Guid userId, string currentPassword, string newPassword, string confirmPassword)
        {
            var user = _userRepo.GetById(userId);
            if (user == null)
                throw new Exception("User not found");

            // Current password verification
            if (user.Password != currentPassword)
                throw new Exception("Current password is incorrect");

            // Check new password matches confirm password
            if (newPassword != confirmPassword)
                throw new Exception("New password and confirm password do not match");

            // Update password
            user.Password = newPassword;

            // Save to database
            _userRepo.Update(user);
        }

    }
}
