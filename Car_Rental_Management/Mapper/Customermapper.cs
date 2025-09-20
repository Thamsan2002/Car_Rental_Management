using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Car_Rental_Management.Mapper
{
    public class Customermapper
    {
        public static Customer ToCustomer(CustomerViewModel vm, Guid userId)
        {
            return new Customer
            {
                FullName = vm.FullName,
                Gender = vm.Gender,
                NationalIdentityCard = vm.NationalIdentityCard,
                Phonenumber = vm.Phonenumber,
                Address = vm.Address,
                UserId = userId
            };
        }
        public static User ToUser(CustomerViewModel vm)
        {
            return new User
            {
                Email = vm.Email,
                PasswordHash = vm.Password
            };
        }


    }
}

