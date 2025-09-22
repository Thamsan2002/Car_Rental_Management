using Car_Rental_Management.Dtos;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.Mapper;

namespace Car_Rental_Management.Service.Implement
{
    public class AdminDashBoardService : IAdminDashBoardService
    {
        private readonly IStaffRepository _staffRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly ICarRepository _carRepo;
        private readonly IDriverRepository _driverRepo;

        public AdminDashBoardService(
            IStaffRepository staffRepo,
            ICustomerRepository customerRepo,
            ICarRepository carRepo,
            IDriverRepository driverRepo)
        {
            _staffRepo = staffRepo;
            _customerRepo = customerRepo;
            _carRepo = carRepo;
            _driverRepo = driverRepo;
        }
        public async Task<AdminDashBoardDto> GetDashboardSummaryAsync()
        {
            var totalCars = await _carRepo.GetCarCountAsync();
            var availableCars = await _carRepo.GetAvailableCarCountAsync();
            var staffCount = await _staffRepo.GetStaffCountAsync();
            var customerCount = await _customerRepo.GetCustomerCountAsync();
            var driverCount = await _driverRepo.GetDriverCountAsync();

            // Use mapper instead of direct assignment
            return AdminDashboardMapper.MapToDto(
                staffCount,
                customerCount,
                totalCars,
                availableCars,
                driverCount
            );
        }
    }
}
