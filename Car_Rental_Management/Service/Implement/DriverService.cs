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

            var existingUser = await _userRepository.GetByEmailAsync(viewModel.Email);
            if (existingUser != null)
            {
                return "User already exists with this email!";
            }


            var user = DriverMapper.ToUser(viewModel);
            var createdUser = await _userRepository.AddAsync(user);

            var driver = DriverMapper.ToDriver(viewModel, createdUser.userId);
            await _driverRepository.AddAsync(driver);

            return "Driver created successfully!";
        }

        public async Task<IEnumerable<Driver>> GetAllDriversAsync()
        {
            return await _driverRepository.GetAllAsync();
        }

    }
}