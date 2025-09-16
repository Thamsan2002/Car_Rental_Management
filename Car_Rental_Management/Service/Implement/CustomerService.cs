
using Car_Rental_Management.Mapper;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using System;

namespace Car_Rental_Management.Service.Implement
{
    public class CustomerService : ICustomerService
    {

        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IUserRepository userRepository, ICustomerRepository customerRepository)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
        }


        public async Task<Customer> GetByIdAsync(Guid customerId)
        {
            return await _customerRepository.GetByIdAsync(customerId);
        }
        public async Task<String> RegisterUserAsync(CustomerSignupViewmodel model)
        {
            // Check if email or phone already exists
            bool exists = await _userRepository.IsEmailOrPhoneExistAsync(model.Email, model.PhoneNumber);

            if (exists)
            {
                return "Customer already exists with this email or phone number!";
            }

            // Map ViewModel → Model
            var customer = new User
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                Role = model.Role,
                
            };

            await _userRepository.AddAsync(customer);

            return "Account created successfully";
        }

        public async Task<User?> LoginCustomerAsync(CustomerLoginViewModel model)
        {
            return await _userRepository.GetCustomerByLoginAsync(model.EmailOrPhone, model.Password);
        }

        public async Task<(bool isSuccess, string errorMessage, Guid? customerId)> RegisterCustomerAsync(CustomerRegisterViewModel model)
        {
            // Check duplicates
            bool exists = await _customerRepository.IsCustomerExistsAsync(model.NationalIdentityCard, model.DrivingLicenseNumber, model.Phonenumber);
            if (exists)
                return (false, "NIC, Driving License, or Phone number already exists", null);

            // Map ViewModel -> Model
            var customer = new Customer
            {
                UserId = Guid.NewGuid(), // Or link to actual User entity if needed
                FullName = model.FullName,
                Gender = model.Gender,
                NationalIdentityCard = model.NationalIdentityCard,
                DrivingLicenseNumber = model.DrivingLicenseNumber,
                Phonenumber = model.Phonenumber,
                Address = model.Address,
                AccountCreateDate = DateTime.Now.ToString("yyyy-MM-dd"),
            };

            // Add to DB
            var addedCustomer = await _customerRepository.AddCustomerAsync(customer);
            return (true, string.Empty, addedCustomer.Id);
        }
    }
}
