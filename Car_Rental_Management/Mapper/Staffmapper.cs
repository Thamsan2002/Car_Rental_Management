using Car_Rental_Management.Models;
using Car_Rental_Management.Dtos;
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
            };
        }

        // ViewModel → User Model
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

        // Staff → List DTO
        public static StaffDto ToListDto(Staff staff)
        {
            return new StaffDto
            {
                Id = staff.Id,
                StaffCode = staff.StaffCode,
                Name = staff.Name,
                Status = staff.Status
            };
        }

        // Staff → Detail DTO (frontend safe)
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
                ShiftTime = staff.ShiftTime,
                EmailAddress = staff.User?.EmailAddress ?? string.Empty,
                PhoneNumber = staff.User?.PhoneNumber ?? string.Empty,
                Role = staff.User?.Role ?? string.Empty,
                // password skip pannittu
            };
        }

        // Update existing staff from ViewModel
        public static void UpdateStaffModel(Staff staff, Staffviewmodel vm)
        {
            staff.Name = vm.Name;
            staff.StaffCode = vm.StaffCode;
            staff.Address = vm.Address;
            staff.Status = vm.Status;
            staff.ProfileImage = vm.ProfileImage;
            staff.Salary = vm.Salary;
            staff.ShiftTime = vm.ShiftTime; // ⚡ Ensure type matches TimeSpan
        }

        // Update existing user from ViewModel
        public static void UpdateUserModel(User user, Staffviewmodel vm)
        {
            user.EmailAddress = vm.EmailAddress;
            user.Password = vm.Password;
            user.PhoneNumber = vm.PhoneNumber;
            user.Role = vm.Role;
        }

        // Detail DTO → ViewModel (edit form)
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
                PhoneNumber = dto.PhoneNumber,
                Role = dto.Role
            };
        }
    }
}
