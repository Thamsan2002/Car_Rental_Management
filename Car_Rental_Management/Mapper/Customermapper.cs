using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;
using Car_Rental_Management.Dtos;

namespace Car_Rental_Management.Mapper
{
    public static class CustomerMapper
    {
        public static User ToUser(CustomerViewModel vm)
        {
            return new User
            {
                userId = Guid.NewGuid(),
                Email = vm.Email,
                Password = vm.Password,
                PhoneNumber = vm.PhoneNumber,
                Role = "Customer"
            };
        }

        public static Customer ToModel(CustomerViewModel vm, Guid userId)
        {
            return new Customer
            {
                Id = vm.Id == Guid.Empty ? Guid.NewGuid() : vm.Id,
                FullName = vm.FullName,
                Gender = vm.Gender,
                NationalIdentityCard = vm.NationalIdentityCard,
                Phonenumber = vm.PhoneNumber,
                Address = vm.Address,
                AccountCreateDate = vm.AccountCreateDate,
                UserId = userId
            };
        }

        public static CustomerDto ToDto(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Gender = customer.Gender,
                NationalIdentityCard = customer.NationalIdentityCard,
                Phonenumber = customer.Phonenumber,
                Address = customer.Address,
                AccountCreateDate = customer.AccountCreateDate,
                Email = customer.User.Email
            };
        }

        public static CustomerViewModel ToViewModel(Customer customer)
        {
            return new CustomerViewModel
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Gender = customer.Gender,
                NationalIdentityCard = customer.NationalIdentityCard,
                PhoneNumber = customer.Phonenumber,
                Address = customer.Address,
                AccountCreateDate = customer.AccountCreateDate,
                Email = customer.User.Email,
                Password = customer.User.Password
            };
        }

        public static void MapViewModelToEntity(CustomerViewModel vm, Customer customer)
        {
            customer.FullName = vm.FullName;
            customer.Gender = vm.Gender;
            customer.NationalIdentityCard = vm.NationalIdentityCard;
            customer.Phonenumber = vm.PhoneNumber;
            customer.Address = vm.Address;
            customer.AccountCreateDate = vm.AccountCreateDate;

            if (customer.User != null)
            {
                customer.User.Email = vm.Email;
                customer.User.Password = vm.Password;
                customer.User.PhoneNumber = vm.PhoneNumber;
            }
        }
    }
}
