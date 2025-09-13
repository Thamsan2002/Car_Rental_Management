using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Mapper
{
    public class AdminMapper
    {
        public static User MapToUser(Adminviewmodel vm)
        {
            User user = new User();
            user.Id = Guid.NewGuid();
            user.Email = vm.Email;
            user.PhoneNumber = vm.PhoneNumber;
            user.Password = vm.Password;
            user.Role = vm.Role;
            return user;
        }

        // 2️⃣ ViewModel → Admin Model
        public static Admin MapToAdmin(Adminviewmodel vm)
        {
            Admin admin = new Admin();
            admin.Id = Guid.NewGuid();
            admin.Name = vm.Name;
            admin.Address = vm.Address;
            return admin;
        }
        public static void UpdateAdminModel(Admin admin, Adminviewmodel vm)
        {
            admin.Name = vm.Name;
            admin.Address = vm.Address;
            if (admin.User != null)
            {
                admin.User.Email = vm.Email;
                admin.User.PhoneNumber = vm.PhoneNumber;
                if (!string.IsNullOrEmpty(vm.Password))
                {
                    admin.User.Password = vm.Password;
                }
            }
        }
        public static AdminDto ToDto(Admin admin)
        {
            return new AdminDto
            {
                Id = admin.Id,
                Name = admin.Name,
                Address = admin.Address,
                Email = admin.User.Email,
                PhoneNumber = admin.User.PhoneNumber,
                Role = admin.User.Role
            };
        }
    }
}
