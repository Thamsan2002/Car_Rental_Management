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
            var existingUser = await _userRepository.IsEmailOrPhoneExistAsync(viewModel.Email, viewModel.EmergencyContact);

            if (existingUser)
            {
                return "Driver already exists with this email and phone number!";
            }

            // Create User
            var user = DriverMapper.ToUser(viewModel);
            var createdUser = await _userRepository.AddAsync(user);

            // Create Driver
            var driver = DriverMapper.ToDriver(viewModel, createdUser);
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
            if (driver == null)
                return null;

            return DriverMapper.ToDto(driver);
        }


        public async Task UpdateDriverAsync(DriverViewModel viewModel)
        {
            // Fetch existing driver
            var driver = await _driverRepository.GetByIdAsync(viewModel.Id);
            if (driver == null)
                throw new Exception("Driver not found!");

            DriverMapper.MapViewModelToEntity(viewModel, driver);

            // Save changes
            await _driverRepository.UpdateAsync(driver);
        }

    }
}