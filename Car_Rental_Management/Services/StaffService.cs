using Car_Rental_Management.Dtos;
using Car_Rental_Management.Intrface;
using Car_Rental_Management.Mapper;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repositories;
using Car_Rental_Management.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        // 🔹 Add staff
        public async Task AddStaffAsync(Staffviewmodel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.EmailAddress))
                throw new ArgumentException("Email is required.");
            if (string.IsNullOrWhiteSpace(vm.Password))
                throw new ArgumentException("Password is required.");

            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(vm.EmailAddress, emailPattern))
                throw new ArgumentException("Invalid email format.");

            var existingUser = await _userRepo.GetUserByEmailAsync(vm.EmailAddress);
            if (existingUser != null)
                throw new InvalidOperationException("User with this email already exists.");

            // Mapper used here
            var userModel = Staffmapper.ToUserModel(vm);
            var savedUser = await _userRepo.AddAsync(userModel);

            var staffModel = Staffmapper.ToStaffModel(vm);
            staffModel.UserId = savedUser;

            staffModel.StaffCode = GenerateStaffCode();

            await _staffRepo.AddAsync(staffModel);
        }

        private string GenerateStaffCode()
        {
            var prefix = "STF";
            var timestamp = DateTime.UtcNow.Ticks % 1000000;
            var random = new Random().Next(100, 999);
            return $"{prefix}{timestamp}{random}";
        }

        // 🔹 Get all staff
        public async Task<List<StaffDto>> GetAllStaffAsync()
        {
            var staffs = await _staffRepo.GetAllAsync();
            return staffs.Select(Staffmapper.ToListDto).ToList();
        }

        // 🔹 Get staff by ID
        public async Task<StaffDetailDto> GetStaffByIdAsync(Guid id)
        {
            var staff = await _staffRepo.GetByIdAsync(id);
            if (staff == null) throw new Exception("Staff not found");

            return Staffmapper.ToDetailDto(staff);
        }

        // 🔹 Delete staff
        public async Task DeleteStaffAsync(Guid id)
        {
            var staff = await _staffRepo.GetByIdAsync(id);
            if (staff == null) throw new Exception("Staff not found");

            await _staffRepo.DeleteByIdAsync(id);
        }

        // 🔹 Update staff
        public async Task UpdateStaffAsync(Guid id, Staffviewmodel vm)
        {
            var staff = await _staffRepo.GetByIdAsync(id);
            if (staff == null) throw new Exception("Staff not found");

            Staffmapper.UpdateStaffModel(staff, vm);
            await _staffRepo.UpdateAsync(staff);

            var user = await _userRepo.GetByIdAsync(staff.UserId);
            if (user != null)
            {
                Staffmapper.UpdateUserModel(user, vm);
                await _userRepo.UpdateAsync(user);
            }
        }
    }
}
