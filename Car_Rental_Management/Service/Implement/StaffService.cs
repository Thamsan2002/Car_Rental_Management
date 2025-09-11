using Car_Rental_Management.Dtos;
using Car_Rental_Management.Mapper;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Implement
{
    public class StaffService : IStaffservice
    {
        private readonly IUserServices _userService;
        private readonly IStaffRepository _staffRepository;

        public StaffService(IUserServices userService, IStaffRepository staffRepository)
        {
            _userService = userService;
            _staffRepository = staffRepository;
        }

        public async Task AddStaffAsync(StaffViewModel vm)
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
                throw new InvalidOperationException("User with this phone number already exists.");

            // Map ViewModel -> User model using your existing mapper
            var userModel = Staffmapper.ToUserModel(vm);


            var user = StaffMapper.ToUser(vm);
            var createdUser = await _userService.CreateUserAsync(user);

            var staff = StaffMapper.ToModel(vm, createdUser.userId);
            await _staffRepository.AddAsync(staff);
        }

        public async Task DeleteStaffAsync(Guid id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            if (staff == null) throw new Exception("Staff not found!");

            await _userService.DeleteUserAsync(staff.UserId);
            await _staffRepository.DeleteAsync(staff);
        }

        public async Task<IEnumerable<StaffDto>> GetAllAsync()
        {
            var staffList = await _staffRepository.GetAllAsync();
            return staffList.Select(StaffMapper.ToDTO);
        }

        public async Task<StaffViewModel?> GetStaffByIdAsync(Guid id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            return staff == null ? null : StaffMapper.ToViewModel(staff);
        }

        public async Task UpdateStaffAsync(Guid id, StaffViewModel vm)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            if (staff == null) throw new Exception("Staff not found!");

            var user = await _userService.GetByIdAsync(staff.UserId);
            if (user == null) throw new Exception("User not found!");

            user.Email = vm.Email;
            user.PhoneNumber = vm.PhoneNumber;
            user.Password = vm.Password;
            await _userService.UpdateUserAsync(user);


            // Update related user (excluding password if needed)
            //var user = await _userRepo.GetByEmailAndPhoneAsync(staff.UserId,staff);
            //if (user != null)
            //{
            //    Staffmapper.UpdateUserModel(user, vm);
            //    await _userRepo.UpdateAsync(user);
            //}

        }
    }
}
