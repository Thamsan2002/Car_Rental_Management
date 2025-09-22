using Car_Rental_Management.Mapper;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using System;
using System.Threading.Tasks;
using BCrypt.Net;

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

        // Get customer by userId
        public async Task<Customer> GetByIdAsync(Guid customerId)
        {
            return await _customerRepository.GetByUserIdAsync(customerId);
        }

        // Register user with hashed password
        public async Task<string> RegisterUserAsync(CustomerSignupViewmodel model)
        {
            bool exists = await _userRepository.IsEmailOrPhoneExistAsync(model.Email, model.PhoneNumber);
            if (exists)
                return "Customer already exists with this email or phone number!";

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = hashedPassword,
                Role = model.Role
            };

            await _userRepository.AddAsync(user);
            return "Account created successfully";
        }

        // Login customer using email/phone + password
        public async Task<User?> LoginCustomerAsync(CustomerLoginViewModel model)
        {
            // Call repository method with email/phone AND hashed password
            var user = await _userRepository.GetByEmailOrPhoneAsync(model.EmailOrPhone, model.Password);

            if (user == null)
                return null;

            // Verify password using BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
            return isPasswordValid ? user : null;
        }

        // Register detailed customer info
        public async Task<(bool isSuccess, string errorMessage, Guid? customerId)> RegisterCustomerAsync(CustomerRegisterViewModel model)
        {
            bool exists = await _customerRepository.IsCustomerExistsAsync(model.NationalIdentityCard, model.DrivingLicenseNumber, model.Phonenumber);
            if (exists)
                return (false, "NIC, Driving License, or Phone number already exists", null);

            var customer = new Customer
            {

                UserId = model.UserId,
                FullName = model.FullName,
                Gender = model.Gender,
                NationalIdentityCard = model.NationalIdentityCard,
                DrivingLicenseNumber = model.DrivingLicenseNumber,
                Phonenumber = model.Phonenumber,
                Address = model.Address,
                AccountCreateDate = DateTime.Now.ToString("yyyy-MM-dd"),
            };

            var addedCustomer = await _customerRepository.AddCustomerAsync(customer);
            return (true, string.Empty, addedCustomer.Id);
        }

        // Get customer details by UserId
        public async Task<CustomerRegisterViewModel?> GetCustomerByUserIdAsync(Guid userId)
        {
            var customer = await _customerRepository.GetByUserIdAsync(userId);
            if (customer == null) return null;

            return new CustomerRegisterViewModel
            {
                UserId = customer.UserId,
                FullName = customer.FullName,
                Address = customer.Address,
                Phonenumber = customer.Phonenumber,
                Gender = customer.Gender,
                NationalIdentityCard = customer.NationalIdentityCard,
                DrivingLicenseNumber = customer.DrivingLicenseNumber
            };
        }
    }
}
