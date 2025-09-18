using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using BCrypt.Net;

namespace Car_Rental_Management.Service.Implement
{
    public class AdminLoginService : IAdminLoginService
    {
        private readonly IUserRepository _userRepo;

        public AdminLoginService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> VerifyAdminLoginAsync(string emailOrPhone, string password)
        {
            var user = await _userRepo.GetByEmailOrPhoneAsync(emailOrPhone);
            if (user == null)
                return false;

            bool verified = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!verified)
                return false;

            if (user.Role != "Admin" && user.Role != "Superadmin")
                return false;

            return true;
        }
    }
}
