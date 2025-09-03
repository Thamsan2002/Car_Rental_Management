using Car_Rental_Management.Dtos;
using Car_Rental_Management.Intrface;
using Car_Rental_Management.Mapper;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repositories;
using Car_Rental_Management.viewmodel;
using System;
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

        public async Task AddStaffAsync(Staffviewmodel vm)
        {
            // 🔹 1️⃣ Empty validation
            if (string.IsNullOrWhiteSpace(vm.EmailAddress))
                throw new ArgumentException("Email is required.");

            if (string.IsNullOrWhiteSpace(vm.Password))
                throw new ArgumentException("Password is required.");

            // 🔹 2️⃣ Email format validation
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(vm.EmailAddress, emailPattern))
                throw new ArgumentException("Invalid email format.");

            // 🔹 3️⃣ Duplicate email check
            var existingUser = await _userRepo.GetUserByEmailAsync(vm.EmailAddress);
            if (existingUser != null)
                throw new InvalidOperationException("User with this email already exists.");

            // 🔹 4️⃣ Create User model using mapper
            var userModel = Staffmapper.ToUserModel(vm);
            var savedUser = await _userRepo.AddAsync(userModel);

            // 🔹 5️⃣ Create Staff model using mapper
            var staffModel = Staffmapper.ToStaffModel(vm);
            staffModel.UserId = savedUser;

            // 🔹 6️⃣ Generate and assign StaffCode
            staffModel.StaffCode = GenerateStaffCode();

            // 🔹 7️⃣ Save Staff to DB
            await _staffRepo.AddAsync(staffModel);
        }

        // 🔧 Private method to generate readable StaffCode
        private string GenerateStaffCode()
        {
            var prefix = "STF";
            var timestamp = DateTime.UtcNow.Ticks % 1000000; // Keeps it short
            var random = new Random().Next(100, 999); // Adds randomness
            return $"{prefix}{timestamp}{random}"; // e.g., STF456789123
        }


        // 🔹 Get all staff for list view
        public async Task<List<StaffDto>> GetAllStaffAsync()
        {
            var staffs = await _staffRepo.GetAllAsync();
            return staffs.Select(Staffmapper.ToListDto).ToList();
        }

        // 🔹 Get one staff by ID
        public async Task<StaffDetailDto> GetStaffByIdAsync(Guid id)
        {
            var staff = await _staffRepo.GetByIdAsync(id);
            if (staff == null) throw new Exception("Staff not found");

            return Staffmapper.ToDetailDto(staff);
        }

        // 🔹 Delete staff by ID
        public async Task DeleteStaffAsync(Guid id)
        {
            var staff = await _staffRepo.GetByIdAsync(id);
            if (staff == null) throw new Exception("Staff not found");

            await _staffRepo.DeleteByIdAsync(id);
        }
        public async Task UpdateStaffAsync(Guid id, Staffviewmodel vm)
        {
            // Get existing staff
            var staff = await _staffRepo.GetByIdAsync(id);
            if (staff == null)
                throw new Exception("Staff not found");

            // Update staff fields using mapper
            Staffmapper.UpdateStaffModel(staff, vm);
            await _staffRepo.UpdateAsync(staff);

            // Get and update linked user
            var user = await _userRepo.GetByIdAsync(staff.UserId);
            if (user != null)
            {
                Staffmapper.UpdateUserModel(user, vm);
                await _userRepo.UpdateAsync(user);
            }
        }

        //public async Task UpdateStaffAsync(Guid id, Staffviewmodel vm)
        //{
        //    var staff = await _staffRepo.GetByIdAsync(id);
        //    if (staff == null)
        //        throw new Exception("Staff not found");

        //    // Map fields from ViewModel to entity
        //    staff.Name = vm.Name;
        //    staff.Email = vm.Email;
        //    staff.Position = vm.Position;
        //    // Add other fields as needed

        //    await _staffRepo.UpdateAsync(staff);
        //}


    }
}
