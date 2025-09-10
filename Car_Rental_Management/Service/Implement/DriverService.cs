using Car_Rental_Management.Dtos;
using Car_Rental_Management.Mapper;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Implement
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;

        public DriverService(IDriverRepository driverRepository, IUserRepository userRepository)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
        }

        public async Task<DriverDto> CreateDriverAsync(DriverViewModel vm)
        {
            // Check if user exists
            var existingUser = await _userRepository.GetByEmailAsync(vm.Email);
            if (existingUser != null)
                throw new Exception("Email already exists!");

            var user = DriverMapper.ToUser(vm);
            var createdUser = await _userRepository.CreateAsync(user);

            var driver = DriverMapper.ToModel(vm, createdUser.userId);
            var createdDriver = await _driverRepository.AddAsync(driver);

            return DriverMapper.ToDto(createdDriver);
        }

        public async Task DeleteDriverAsync(Guid id)
        {
            var driver = await _driverRepository.GetByIdAsync(id);
            if (driver == null) throw new Exception("Driver not found!");

            var user = await _userRepository.GetByIdAsync(driver.UserId);
            if (user != null)
                await _userRepository.DeleteAsync(user);

            await _driverRepository.DeleteAsync(driver);
        }

        public async Task<IEnumerable<DriverDto>> GetAllDriversAsync()
        {
            var drivers = await _driverRepository.GetAllAsync();
            return drivers.Select(DriverMapper.ToDto);
        }

        public async Task<DriverDto?> GetDriverByIdAsync(Guid id)
        {
            var driver = await _driverRepository.GetByIdAsync(id);
            return driver == null ? null : DriverMapper.ToDto(driver);
        }

        public async Task<DriverDto> UpdateDriverAsync(DriverViewModel vm)
        {
            var driver = await _driverRepository.GetByIdAsync(vm.Id);
            if (driver == null) throw new Exception("Driver not found!");

            var user = await _userRepository.GetByIdAsync(driver.UserId);
            if (user == null) throw new Exception("User not found!");

            // Update User
            user.Email = vm.Email;
            user.PhoneNumber = vm.PhoneNumber;
            user.Password = vm.Password;
            await _userRepository.UpdateAsync(user);

            // Update Driver
            DriverMapper.MapViewModelToEntity(vm, driver);
            var updatedDriver = await _driverRepository.UpdateAsync(driver);

            return DriverMapper.ToDto(updatedDriver);
        }
    }
}
