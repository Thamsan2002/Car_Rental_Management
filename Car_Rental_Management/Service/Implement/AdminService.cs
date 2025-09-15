using Car_Rental_Management.Dtos;
using Car_Rental_Management.Mapper;
using Car_Rental_Management.Models;
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
        // Update Admin
        public async Task UpdateAdminAsync(Guid Id, Adminviewmodel vm)
        {
            var admin = await _adminRepo.GetByIdAsync(Id);
            if (admin == null) throw new Exception("Admin not found");

            // Update Admin + User in memory
            AdminMapper.UpdateAdminModel(admin, vm);

            // Save changes to User table
            await _userRepo.UpdateAsync(admin.User);

            // Save changes to Admin table
            await _adminRepo.UpdateAdminAsync(admin);
        }
        public async Task<List<AdminDto>> GetAllAdminsAsync()
        {
            var admins = await _adminRepo.GetAllAsync(); // List<Admin> including User
            var adminDtoList = admins.Select(AdminMapper.ToDto).ToList();
            return adminDtoList;
        }
        public async Task<AdminDto> GetAdminByIdAsync(Guid id)
        {
            var admin = await _adminRepo.GetByIdAsync(id);
            if (admin == null) throw new Exception("Admin not found");

            return AdminMapper.ToDto(admin);
        }

        public async Task DeleteAdminAsync(Guid id)
        {
            // Fetch admin from repo
            var admin = await _adminRepo.GetByIdAsync(id);
            if (admin == null)
                throw new Exception("Admin not found"); // not found case

            // Call repo to delete admin + related user
            await _adminRepo.DeleteAdminAsync(id);
        }

    }
}
