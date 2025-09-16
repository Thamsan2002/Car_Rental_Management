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
                PhoneNumber = vm.PhoneNumber,
                Password = vm.Password,
                Role = "Driver"
            };
        }

        public static Driver ToDriver(DriverViewModel vm, User user)
        {
            return new Driver
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                User = user,
                Name = vm.Name,
                EmergencyContact = vm.EmergencyContact,
                Nic = vm.Nic,
                Gender = vm.Gender,
                Address = vm.Address,
                LicenseNumber = vm.LicenseNumber,
                LicenseExpiryDate = vm.LicenseExpiryDate,
                Experience = vm.Experience,
                VehicleType = vm.VehicleType
            };
        }

        public static DriverDto ToDriverDto(Driver driver)
        {
            return new DriverDto
            {
                Id = driver.Id,
                Name = driver.Name,
                PhoneNumber = driver.User.PhoneNumber,
                EmergencyContact = driver.EmergencyContact,
                Nic = driver.Nic,
                Gender = driver.Gender,
                Address = driver.Address,
                LicenseNumber = driver.LicenseNumber,
                LicenseExpiryDate = driver.LicenseExpiryDate,
                Experience = driver.Experience,
                VehicleType = driver.VehicleType,
                Email = driver.User.Email
            };
        }

        public static void UpdateDriverFromViewModel(Driver driver, DriverViewModel vm)
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
            driver.User.PhoneNumber = vm.PhoneNumber;
            driver.User.Email = vm.Email;
            driver.User.Password = vm.Password;
        }
    }
}