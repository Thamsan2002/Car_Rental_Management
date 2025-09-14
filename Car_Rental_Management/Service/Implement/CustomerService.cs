
using Car_Rental_Management.Mapper;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;

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

        public async Task<string> CreateCustomerAsync(CustomerViewModel viewModel)
        {

            var existingUser = await _userRepository.GetByEmailAndPhoneAsync(viewModel.Email, viewModel.Phonenumber);
            if (existingUser != null)
            {
                return "User already exists with this email!";
            }


            var user = Customermapper.ToUser(viewModel);
            var createdUser = await _userRepository.AddAsync(user);

            var customer = Customermapper.ToCustomer(viewModel, createdUser);
            await _customerRepository.AddAsync(customer);

            return "Customer created successfully!";
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

    }
}
