using Car_Rental_Management.Mapper;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Implement
{
    public class AdminService: IAdminService
    {
        private readonly IUserRepository _userRepo;
        private readonly IAdminrepository _adminRepo;

        public AdminService(IUserRepository userRepo, IAdminrepository adminRepo)
        {
            _userRepo = userRepo;
            _adminRepo = adminRepo;
        }

        public async Task AddAdminAsync(Adminviewmodel vm)
        {
            

            // Duplicate check
            var existingUser = await _userRepo.GetByEmailAndPhoneAsync(vm.Email, vm.PhoneNumber);
            if (existingUser != null)
                throw new InvalidOperationException("User with this email or phone number already exists.");

            // Map ViewModel → User
            var userModel = AdminMapper.MapToUser(vm);

            // Save User
            var savedUserId = await _userRepo.AddAsync(userModel);

            // Map ViewModel → Admin
            var adminModel = AdminMapper.MapToAdmin(vm);
            adminModel.UserId = savedUserId;

            // Save Admin
            await _adminRepo.AddAdminAsync(adminModel);

            
        }
    }
}
