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
    }
}
