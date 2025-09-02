    using Car_Rental_Management.Models;
    using Car_Rental_Management.viewmodel;

namespace Car_Rental_Management.Mapper
{
    public class Staffmapper
    {
        // ViewModel → Staff Model
        public static Staff ToStaffModel(Staffviewmodel vm)
        {
            return new Staff
            {
                Id = Guid.NewGuid(),
                Name = vm.Name,
                Address = vm.Address,
                Status = vm.Status,
                ProfileImage = vm.ProfileImage,
                Salary = vm.Salary,
                ShiftTime = vm.ShiftTime,
                 // User.Id is Guid
            };
        }

        // ViewModel → Model
        public static User ToUserModel(Staffviewmodel vm)
        {
            return new User
            {
                EmailAddress = vm.EmailAddress,
                Password = vm.Password,
                PhoneNumber = vm.PhoneNumber,
                Role = vm.Role
            };
        }


    }
}   
