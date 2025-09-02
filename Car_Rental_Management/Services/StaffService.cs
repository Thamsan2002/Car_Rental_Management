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
            staffModel.Id = savedUser;

            // 🔹 6️⃣ Save Staff to DB
            await _staffRepo.AddAsync(staffModel);
        }
    }
}
