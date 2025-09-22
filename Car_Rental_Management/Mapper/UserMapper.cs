using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Mapper
{
    public class UserMapper
    {
        public static User ToModel(UserViewModel vm, Guid userId)
        {
            return new User
            {
                Id = userId,          // assign logged-in user ID
                PhoneNumber = vm.PhoneNumber,
                Email = vm.Email,
                PasswordHash = vm.NewPassword  // service will check if empty and preserve old password
            };
        }
        //public static UserViewModel ToViewModel(User user)
        //{
        //    return new UserViewModel
        //    {
        //        Email = user.Email,
        //        Phonenumber = user.PhoneNumber
        //        // Password fields empty by default in ViewModel
        //    };
        //}
    }
}
