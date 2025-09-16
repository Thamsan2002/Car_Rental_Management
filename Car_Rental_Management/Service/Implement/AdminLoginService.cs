using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;

namespace Car_Rental_Management.Service.Implement
{
    public class AdminLoginService: IAdminLoginService
    {
      
        
            private readonly IUserRepository _userRepo;
            private readonly IAdminrepository _adminRepo;
            public AdminLoginService(IUserRepository userRepo, IAdminrepository adminRepo)
            {
                _userRepo = userRepo;
                _adminRepo = adminRepo;
            }
            public async Task<bool> VerifyAdminLoginAsync(string emailOrPhone, string password)
            {
                // 1️⃣ Fetch user by email or phone
                var user = await _userRepo.GetByEmailOrPhoneAsync(emailOrPhone, password);
                if (user == null)
                    return false;

                // 2️⃣ Check password
                if (user.Password != password)
                    return false;

                // 3️⃣ Check role
                if (user.Role != "Admin")
                    return false;

                // 4️⃣ Check if user has an Admin record
                var admin = await _adminRepo.GetByUserIdAsync(user.Id);
                return admin != null;
            }

        
    }
}
