using Car_Rental_Management.Dtos;

using Car_Rental_Management.Mapper;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.viewmodel;
using Car_Rental_Management.ViewModel;
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

        //  Add staff
        public async Task AddStaffAsync(Staffviewmodel vm)
        {
            if (vm == null)
                throw new ArgumentNullException(nameof(vm));

            // Check if email already exists
            var existingEmailUser = await _userRepo.GetByEmailAndPhoneAsync(vm.EmailAddress, vm.PhoneNumber);
            if (existingEmailUser != null)
                throw new InvalidOperationException("User with this email already exists.");

            // Check if phone number already exists
            //var existingPhoneUser = await _userRepo.GetByPhoneAsync(vm.PhoneNumber);
            //if (existingPhoneUser != null)
            //    throw new InvalidOperationException("User with this phone number already exists.");

            // Map ViewModel -> User model using your existing mapper
            var userModel = Staffmapper.ToUserModel(vm);

            // Save User to DB
            var savedUserId = await _userRepo.AddAsync(userModel);
            if (savedUserId == Guid.Empty)
                throw new Exception("Failed to save user record.");

            // Map ViewModel -> Staff model using your mapper
            var staffModel = Staffmapper.ToStaffModel(vm);
            staffModel.UserId = savedUserId;

            // Generate staff code
            staffModel.StaffCode = await GenerateStaffCode();

            // Save Staff to DB
            await _staffRepo.AddAsync(staffModel);
        }


        private async Task<string> GenerateStaffCode()
        {
            var lastStaff = (await _staffRepo.GetAllAsync())
                            .OrderByDescending(s => s.StaffCode)
                            .FirstOrDefault();

            int newNumber = 1; // default for first staff
            if (lastStaff != null &&
                int.TryParse(lastStaff.StaffCode.Replace("STF", ""), out int lastNumber))
            {
                newNumber = lastNumber + 1;
            }

            return $"STF{newNumber:D3}"; // STF001, STF002, etc.
        }

        //  Get all staff
        public async Task<List<StaffDto>> GetAllStaffAsync()
        {
            var staffs = await _staffRepo.GetAllAsync();
            return staffs.Select(Staffmapper.ToListDto).ToList();
        }

        //  Get staff by ID
        public async Task<StaffDetailDto> GetStaffByIdAsync(Guid id)
        {
            var staff = await _staffRepo.GetByIdAsync(id);
            if (staff == null) throw new Exception("Staff not found");

            return Staffmapper.ToDetailDto(staff);
        }

        //  Delete staff
        public async Task DeleteStaffAsync(Guid id)
        {
            var staff = await _staffRepo.GetByIdAsync(id);
            if (staff == null) throw new Exception("Staff not found");

            await _staffRepo.DeleteByIdAsync(id);
        }

        //  Update staff
        public async Task UpdateStaffAsync(Guid id, Staffviewmodel vm)
        {
            var staff = await _staffRepo.GetByIdAsync(id);
            if (staff == null) throw new Exception("Staff not found");

            // Update staff model
            Staffmapper.UpdateStaffModel(staff, vm);
            await _staffRepo.UpdateAsync(staff);

            //// Update related user (excluding password if needed)
            //var user = await _userRepo.GetByIdAsync(staff.UserId);
            //if (user != null)
            //{
            //    Staffmapper.UpdateUserModel(user, vm);
            //    await _userRepo.UpdateAsync(user);
            //}
        }
    }
}
