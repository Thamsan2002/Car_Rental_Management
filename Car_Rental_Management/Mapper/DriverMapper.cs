using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;
using Car_Rental_Management.Dtos;

namespace Car_Rental_Management.Mapper
{
    public static class DriverMapper
    {
        // ViewModel → Model
        public static Driver ToModel(DriverViewModel vm, Guid userId)
        {
            return new Driver
            {
                Id = vm.Id == Guid.Empty ? Guid.NewGuid() : vm.Id,
                Name = vm.Name,
                EmergencyContact = vm.EmergencyContact,
                Nic = vm.Nic,
                Gender = vm.Gender,
                Address = vm.Address,
                LicenseNumber = vm.LicenseNumber,
                LicenseExpiryDate = vm.LicenseExpiryDate,
                Experience = vm.Experience,
                VehicleType = vm.VehicleType,
                UserId = userId
            };
        }

        // Model → DTO
        public static DriverDto ToDto(Driver driver)
        {
            return new DriverDto
            {
                Id = driver.Id,
                Name = driver.Name,
                EmergencyContact = driver.EmergencyContact,
                Nic = driver.Nic,
                Gender = driver.Gender,
                Address = driver.Address,
                LicenseNumber = driver.LicenseNumber,
                LicenseExpiryDate = driver.LicenseExpiryDate.ToString("yyyy-MM-dd"),
                Experience = driver.Experience,
                VehicleType = driver.VehicleType,
                Email = driver.User.Email,
                PhoneNumber = driver.User.PhoneNumber
            };
        }

        // DTO → ViewModel
        public static DriverViewModel ToViewModel(DriverDto dto)
        {
            return new DriverViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                EmergencyContact = dto.EmergencyContact,
                Nic = dto.Nic,
                Gender = dto.Gender,
                Address = dto.Address,
                LicenseNumber = dto.LicenseNumber,
                LicenseExpiryDate = DateTime.Parse(dto.LicenseExpiryDate),
                Experience = dto.Experience,
                VehicleType = dto.VehicleType,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };
        }

        // Update existing Model with ViewModel data
        public static void MapViewModelToEntity(DriverViewModel vm, Driver driver)
        {
            driver.Name = vm.Name;
            driver.EmergencyContact = vm.EmergencyContact;
            driver.Nic = vm.Nic;
            driver.Gender = vm.Gender;
            driver.Address = vm.Address;
            driver.LicenseNumber = vm.LicenseNumber;
            driver.LicenseExpiryDate = vm.LicenseExpiryDate;
            driver.Experience = vm.Experience;
            driver.VehicleType = vm.VehicleType;

            if (driver.User != null)
            {
                driver.User.Email = vm.Email;
                driver.User.Password = vm.Password;
                driver.User.PhoneNumber = vm.PhoneNumber;
            }
        }

        // Convert ViewModel → User entity
        public static User ToUser(DriverViewModel vm)
        {
            return new User
            {
                userId = Guid.NewGuid(),
                Email = vm.Email,
                Password = vm.Password,
                PhoneNumber = vm.PhoneNumber,
                Role = "Driver"
            };
        }
    }
}
