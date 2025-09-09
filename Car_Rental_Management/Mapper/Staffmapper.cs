using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Mapper
{
    public static class StaffMapper
    {
        public static StaffDto ToDTO(Staff staff)
        {
            return new StaffDto
            {
                staffId = staff.staffId,
                Name = staff.Name,
                Address = staff.Address,
                Status = staff.Status,
                ProfileImage = staff.ProfileImage,
                Salary = staff.Salary,
                ShiftTime = staff.ShiftTime,
                Email = staff.User.Email,
                PhoneNumber = staff.User.PhoneNumber
            };
        }

        public static StaffViewModel ToViewModel(Staff staff)
        {
            return new StaffViewModel
            {
                Id = staff.staffId,
                Name = staff.Name,
                Address = staff.Address,
                Status = staff.Status,
                ProfileImage = staff.ProfileImage,
                Salary = staff.Salary,
                ShiftTime = staff.ShiftTime,
                Email = staff.User.Email,
                PhoneNumber = staff.User.PhoneNumber,
                Password = staff.User.Password
            };
        }

        public static Staff ToModel(StaffViewModel vm, Guid userId)
        {
            return new Staff
            {
                staffId = vm.Id == Guid.Empty ? Guid.NewGuid() : vm.Id,
                Name = vm.Name,
                Address = vm.Address,
                Status = vm.Status,
                ProfileImage = vm.ProfileImage,
                Salary = vm.Salary,
                ShiftTime = vm.ShiftTime,
                UserId = userId
            };
        }

        public static User ToUser(StaffViewModel vm)
        {
            return new User
            {
                userId = Guid.NewGuid(),
                Email = vm.Email,
                Password = vm.Password,
                PhoneNumber = vm.PhoneNumber,
                Role = "Staff"
            };
        }

     
        public static void MapViewModelToEntity(StaffViewModel vm, Staff staff)
        {
            staff.Name = vm.Name;
            staff.Address = vm.Address;
            staff.Status = vm.Status;
            staff.ProfileImage = vm.ProfileImage;
            staff.Salary = vm.Salary;
            staff.ShiftTime = vm.ShiftTime;

            if (staff.User != null)
            {
                staff.User.Email = vm.Email;
                staff.User.Password = vm.Password;
                staff.User.PhoneNumber = vm.PhoneNumber;
            }
        }
    }
}
