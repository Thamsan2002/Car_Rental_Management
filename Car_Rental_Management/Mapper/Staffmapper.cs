using Car_Rental_Management.Dtos;
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
                StaffCode = vm.StaffCode,
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
        // 🔹 Convert Staff model to StaffListDto
        public static StaffDto ToListDto(Staff staff)
        {
            return new StaffDto
            {
                StaffCode = staff.StaffCode,
                Name = staff.Name,
                Status = staff.Status
            };
        }

        // 🔹 Convert Staff model to StaffDetailDto
        public static StaffDetailDto ToDetailDto(Staff staff)
        {
            return new StaffDetailDto
            {
                
                StaffCode = staff.StaffCode,
                Name = staff.Name,
                Address = staff.Address,
                Status = staff.Status,
                ProfileImage = staff.ProfileImage,
                Salary = staff.Salary,
                //ShiftTime = staff.ShiftTime
            };
        }
        public static void UpdateStaffModel(Staff staff, Staffviewmodel vm)
        {
            staff.Name = vm.Name;
            staff.StaffCode = vm.StaffCode;
            staff.Address = vm.Address;
            staff.Status = vm.Status;
            staff.ProfileImage = vm.ProfileImage;
            staff.Salary = vm.Salary;
            staff.ShiftTime = vm.ShiftTime;
        }

        public static void UpdateUserModel(User user, Staffviewmodel vm)
        {
            user.EmailAddress = vm.EmailAddress;
            user.Password = vm.Password;
            user.PhoneNumber = vm.PhoneNumber;
            user.Role = vm.Role;
        }
        public static Staffviewmodel ToViewModel(StaffDetailDto dto)
        {
            return new Staffviewmodel
            {
                Name = dto.Name,
                StaffCode = dto.StaffCode,
                Address = dto.Address,
                Status = dto.Status,
                ProfileImage = dto.ProfileImage,
                Salary = dto.Salary,
                ShiftTime = dto.ShiftTime,
                EmailAddress = dto.EmailAddress,
                Password = dto.Password,
                PhoneNumber = dto.PhoneNumber,
                Role = dto.Role
            };
        }




    }
}   
