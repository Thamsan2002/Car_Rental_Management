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
        private readonly IUserRepository _userRepository;
        private readonly IDriverRepository _driverRepository;

        public DriverService(IUserRepository userRepository, IDriverRepository driverRepository)
        {
            _userRepository = userRepository;
            _driverRepository = driverRepository;
        }

        public async Task<string> CreateDriverAsync(DriverViewModel vm)
        {
            var existingUser = await _userRepository.GetByEmailAndPhoneAsync(vm.Email, vm.EmergencyContact);
            if (existingUser != null)
                return "Driver already exists!";

            var user = DriverMapper.ToUser(vm);
            var createdUser = await _userRepository.CreateAsync(user);

            var driver = DriverMapper.ToDriver(vm, createdUser.userId);
            await _driverRepository.AddAsync(driver);

            return "Driver created successfully!";
        }

        public async Task<IEnumerable<DriverDto>> GetAllDriversAsync()
        {
            var drivers = await _driverRepository.GetAllAsync();
            return drivers.Select(DriverMapper.ToDto);
        }

        public async Task<DriverDto> GetDriverByIdAsync(Guid id)
        {
            var driver = await _driverRepository.GetByIdAsync(id);
            if (driver == null) return null;
            return DriverMapper.ToDto(driver);
        }

        public async Task UpdateDriverAsync(DriverViewModel vm)
        {
            var driver = await _driverRepository.GetByIdAsync(vm.Id);
            if (driver == null) throw new Exception("Driver not found!");

            var user = await _userRepository.GetByIdAsync(driver.UserId);
            if (user == null) throw new Exception("User not found!");

            DriverMapper.MapViewModelToEntity(vm, driver);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}
