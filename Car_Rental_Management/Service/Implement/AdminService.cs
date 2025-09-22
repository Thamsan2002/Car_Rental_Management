using Car_Rental_Management.Dtos;
using Car_Rental_Management.Mapper;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Car_Rental_Management.Service.Implement
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepo;
        private readonly IAdminrepository _adminRepo;

        public AdminService(IUserRepository userRepo, IAdminrepository adminRepo)
        {
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _adminRepo = adminRepo ?? throw new ArgumentNullException(nameof(adminRepo));
        }

        public async Task AddAdminAsync(Adminviewmodel vm)
        {
            // Duplicate check
            var existingUser = await _userRepo.IsEmailOrPhoneExistAsync(vm.Email, vm.PhoneNumber);
            if (existingUser)
                throw new InvalidOperationException("User with this email or phone number already exists.");

            // Hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(vm.Password);

            // Map ViewModel → User
            var userModel = AdminMapper.MapToUser(vm);
            userModel.PasswordHash = hashedPassword; // save hashed password

            // Save User
            var savedUserId = await _userRepo.AddAsync(userModel);

            // Map ViewModel → Admin
            var adminModel = AdminMapper.MapToAdmin(vm);
            adminModel.UserId = savedUserId;

            // Save Admin
            await _adminRepo.AddAdminAsync(adminModel);
        }

        public async Task UpdateAdminAsync(Guid Id, Adminviewmodel vm)
        {
            var admin = await _adminRepo.GetByIdAsync(Id);
            if (admin == null) throw new Exception("Admin not found");

            // Update Admin + User in memory
            AdminMapper.UpdateAdminModel(admin, vm);

            // Update password if provided
            if (!string.IsNullOrEmpty(vm.Password))
            {
                admin.User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(vm.Password);
            }

            // Save changes
            await _userRepo.UpdateAsync(admin.User);
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
            var admin = await _adminRepo.GetByIdAsync(id);
            if (admin == null) throw new Exception("Admin not found");

            await _adminRepo.DeleteAdminAsync(id);
            await _userRepo.DeleteAsync(admin.UserId); // delete related User
        }
    }
}
