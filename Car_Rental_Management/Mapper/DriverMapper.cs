using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Management.Mapper
{
    public class DriverMapper
    {
        public static User ToUser(DriverViewModel vm)
        {
            return new User
            {
                Email = vm.Email,
                Password = vm.Password
            };
        }

        public static Driver ToDriver(DriverViewModel vm, Guid userId)
        {
            return new Driver
            {
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
        public static DriverViewModel ToViewModel(DriverDto dto)
        {
            return new DriverViewModel
            {
                Name = dto.Name,
                Email = dto.Email,
                EmergencyContact = dto.EmergencyContact,
                Nic = dto.Nic,
                Gender = dto.Gender,
                Address = dto.Address,
                LicenseNumber = dto.LicenseNumber,
                LicenseExpiryDate = DateTime.Parse(dto.LicenseExpiryDate),
                Experience = dto.Experience,
                VehicleType = dto.VehicleType
            };
        }

        public static DriverDto ToDto(Driver driver)
        {
            return new DriverDto
            {
                Id = driver.Id,
                Name = driver.Name,
                Email = driver.User.Email,
                EmergencyContact = driver.EmergencyContact,
                Nic = driver.Nic,
                Gender = driver.Gender,
                Address = driver.Address,
                LicenseNumber = driver.LicenseNumber,
                LicenseExpiryDate = driver.LicenseExpiryDate.ToString("yyyy-MM-dd"),
                Experience = driver.Experience,
                VehicleType = driver.VehicleType
            };
        }

        public static void MapViewModelToEntity(DriverViewModel viewModel, Driver driver)
        {
            driver.Name = viewModel.Name;
            driver.EmergencyContact = viewModel.EmergencyContact;
            driver.Nic = viewModel.Nic;
            driver.Gender = viewModel.Gender;
            driver.Address = viewModel.Address;
            driver.LicenseNumber = viewModel.LicenseNumber;
            driver.LicenseExpiryDate = viewModel.LicenseExpiryDate;
            driver.Experience = viewModel.Experience;
            driver.VehicleType = viewModel.VehicleType;

            if (driver.User != null)
            {
                driver.User.Email = viewModel.Email;
                driver.User.Password = viewModel.Password;
            }
        }
    }
}