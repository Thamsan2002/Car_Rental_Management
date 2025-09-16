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

        public async Task<string> CreateDriverAsync(DriverViewModel viewModel)
        {
            var existingUser = await _userRepository.IsEmailOrPhoneExistAsync(viewModel.Email, viewModel.PhoneNumber);

            if (existingUser)
                return "Driver already exists with this email or phone number!";

            var user = DriverMapper.ToUser(viewModel);
            var createdUserId = await _userRepository.AddAsync(user);

            var driver = DriverMapper.ToDriver(viewModel, user);
            await _driverRepository.AddAsync(driver);

            return "Driver created successfully!";
        }

        public async Task<List<DriverDto>> GetAllDriversAsync()
        {
            var drivers = await _driverRepository.GetAllAsync();
            return drivers.Select(DriverMapper.ToDriverDto).ToList();
        }

        public async Task<DriverDto?> GetDriverByIdAsync(Guid id)
        {
            var driver = await _driverRepository.GetByIdAsync(id);
            return driver == null ? null : DriverMapper.ToDriverDto(driver);
        }

        public async Task<string> UpdateDriverAsync(DriverViewModel viewModel)
        {
            var driver = await _driverRepository.GetByIdAsync(viewModel.Id);
            if (driver == null) return "Driver not found!";

            DriverMapper.UpdateDriverFromViewModel(driver, viewModel);
            await _driverRepository.UpdateAsync(driver);
            await _userRepository.UpdateAsync(driver.User);

            return "Driver updated successfully!";
        }

        public async Task<string> DeleteDriverAsync(Guid id)
        {
            var driver = await _driverRepository.GetByIdAsync(id);
            if (driver == null) return "Driver not found!";

            await _driverRepository.DeleteAsync(driver);
            await _userRepository.UpdateAsync(driver.User); // optional: handle user deletion separately
            return "Driver deleted successfully!";
        }

    }
}