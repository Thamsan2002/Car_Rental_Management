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
        private readonly IUserRepository _userRepository;
        private readonly IStaffRepository _staffRepository;

        public StaffService(IUserRepository userRepository, IStaffRepository staffRepository)
        {
            _userRepository = userRepository;
            _staffRepository = staffRepository;
        }

        public async Task AddStaffAsync(StaffViewModel vm)
        {
            var existingUser = await _userRepository.GetByEmailAsync(vm.Email);
            if (existingUser != null)
                throw new Exception("User already exists!");

            var user = StaffMapper.ToUser(vm);
            var createdUser = await _userRepository.CreateAsync(user);

            var staff = StaffMapper.ToModel(vm, createdUser.userId);
            await _staffRepository.AddAsync(staff);
        }

        public async Task DeleteStaffAsync(Guid id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);
            if (staff == null)
                throw new Exception("Staff not found!");

            var user = await _userRepository.GetByIdAsync(staff.UserId);
            if (user != null)
                await _userRepository.DeleteAsync(user);

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

            var user = await _userRepository.GetByIdAsync(staff.UserId);
            if (user == null) throw new Exception("User not found!");

            user.Email = vm.Email;
            user.PhoneNumber = vm.PhoneNumber;
            user.Password = vm.Password;
            await _userRepository.UpdateAsync(user);

            StaffMapper.MapViewModelToEntity(vm, staff);
            await _staffRepository.UpdateAsync(staff);
        }
    }
}
