using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Mapper
{
    public static class StaffMapper
    {
        // Model → DTO
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
                Email = staff.User?.Email,
                PhoneNumber = staff.User?.PhoneNumber
            };
        }

        // Model → ViewModel
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
                Email = staff.User?.Email,
                PhoneNumber = staff.User?.PhoneNumber,
                Password = staff.User?.Password
            };
        }

        // ViewModel → Model
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

        // ViewModel → User (User table ku insert panna)
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

        // Map ViewModel data to existing Model (Update use)
        public static void MapViewModelToEntity(StaffViewModel vm, Staff staff)
        {
            staff.Name = vm.Name;
            staff.Address = vm.Address;
            staff.Status = vm.Status;
            staff.ProfileImage = vm.ProfileImage;
            staff.Salary = vm.Salary;
            staff.ShiftTime = vm.ShiftTime;
        }
    }
}
