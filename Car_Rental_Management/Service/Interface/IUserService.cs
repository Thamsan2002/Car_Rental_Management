using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Interface
{
    public interface IUserService
    {


        User GetUserById(Guid userId);
        bool VerifyPassword(Guid userId, string password);
        void UpdateProfile(Guid userId, UserViewModel vm);
       
        void ChangePassword(Guid userId, string currentPassword, string newPassword, string confirmPassword);


    }



}

