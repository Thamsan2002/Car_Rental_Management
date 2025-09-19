using Car_Rental_Management.Mapper;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.viewmodel;
using Car_Rental_Management.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Car_Rental_Management.Services
{
    public class StaffService : IStaffservice
    {
        private readonly IUserRepository _userRepo;
        private readonly IStaffRepository _staffRepo;

        public StaffService(IUserRepository userRepo, IStaffRepository staffRepo)
        {
            _userRepo = userRepo;
            _staffRepo = staffRepo;
        }

        // Add Staff
        public async Task AddStaffAsync(Staffviewmodel vm)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));

            // Check if email or phone exists
            bool exists = await _userRepo.IsEmailOrPhoneExistAsync(vm.EmailAddress, vm.PhoneNumber);
            if (exists) throw new InvalidOperationException("User with this email or phone already exists.");

            // Map ViewModel -> User model
            var userModel = Staffmapper.ToUserModel(vm);

            // Hash the password before saving
            userModel.PasswordHash = BCrypt.Net.BCrypt.HashPassword(vm.Password);

            // Save User
            var savedUserId = await _userRepo.AddAsync(userModel);

            // Map ViewModel -> Staff model
            var staffModel = Staffmapper.ToStaffModel(vm);
            staffModel.UserId = savedUserId;

            // Generate staff code
            staffModel.StaffCode = await GenerateStaffCode();

            // Save Staff
            await _staffRepo.AddAsync(staffModel);
        }

        private async Task<string> GenerateStaffCode()
        {
            var lastStaff = (await _staffRepo.GetAllAsync())
                            .OrderByDescending(s => s.StaffCode)
                            .FirstOrDefault();

            int newNumber = 1;
            if (lastStaff != null && int.TryParse(lastStaff.StaffCode.Replace("STF", ""), out int lastNumber))
            {
                newNumber = lastNumber + 1;
            }

            return $"STF{newNumber:D3}";
        }

        // Get all staff
        public async Task<List<StaffDto>> GetAllStaffAsync()
        {
            var staffs = await _staffRepo.GetAllAsync();
            return staffs.Select(Staffmapper.ToListDto).ToList();
        }

        // Get staff by ID
        public async Task<StaffDetailDto> GetStaffByIdAsync(Guid id)
        {
            var staff = await _staffRepo.GetByIdAsync(id);
            if (staff == null) throw new Exception("Staff not found");

            return Staffmapper.ToDetailDto(staff);
        }

        // Update staff
        public async Task UpdateStaffAsync(Guid id, Staffviewmodel vm)
        {
            var staff = await _staffRepo.GetByIdAsync(id);
            if (staff == null) throw new Exception("Staff not found");

            // Update Staff model
            Staffmapper.UpdateStaffModel(staff, vm);
            await _staffRepo.UpdateAsync(staff);

            // Update related user
            var user = await _userRepo.GetByIdAsync(staff.UserId);
            if (user != null)
            {
                Staffmapper.UpdateUserModel(user, vm);

                // Hash password if changed
                if (!string.IsNullOrEmpty(vm.Password))
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(vm.Password);

                await _userRepo.UpdateAsync(user);
            }
        }

        // Delete staff
        public async Task DeleteStaffAsync(Guid id)
        {
            var staff = await _staffRepo.GetByIdAsync(id);
            if (staff == null) throw new Exception("Staff not found");

            await _staffRepo.DeleteByIdAsync(id);
        }
    }
}
