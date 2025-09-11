using Car_Rental_Management.Dtos;
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

        // Add new customer
        public async Task<CustomerDto> AddCustomerAsync(CustomerViewModel vm)
        {

            // Check if user already exists (email or phone)
            var existingUser = await _userRepository.GetByEmailAndPhoneAsync(viewModel.Email, viewModel.Phonenumber);

            if (existingUser != null)
                throw new Exception("Email already registered");

            // Map ViewModel -> User
            var user = CustomerMapper.ToUser(vm);
            var createdUser = await _userRepository.CreateAsync(user);

            // Map ViewModel -> Customer
            var customer = CustomerMapper.ToModel(vm, createdUser.userId);
            var createdCustomer = await _customerRepository.AddAsync(customer);


            // Map Customer -> DTO
            return CustomerMapper.ToDto(createdCustomer);
        }

        // Get all customers
        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(CustomerMapper.ToDto);
        }

        // Get customer by Id
        public async Task<CustomerViewModel?> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return customer == null ? null : CustomerMapper.ToViewModel(customer);
        }

        // Update customer
        public async Task<CustomerDto> UpdateCustomerAsync(CustomerViewModel vm)
        {
            var customer = await _customerRepository.GetByIdAsync(vm.Id);
            if (customer == null)
                throw new Exception("Customer not found");

            var user = await _userRepository.GetByIdAsync(customer.UserId);
            if (user == null)
                throw new Exception("User not found");

            // Update User info
            user.Email = vm.Email;
            user.PhoneNumber = vm.PhoneNumber;
            user.Password = vm.Password;
            await _userRepository.UpdateAsync(user);

            // Update Customer info
            CustomerMapper.MapViewModelToEntity(vm, customer);
            var updatedCustomer = await _customerRepository.UpdateAsync(customer);

            return CustomerMapper.ToDto(updatedCustomer);
        }

        // Delete customer
        public async Task DeleteCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                throw new Exception("Customer not found");

            var user = await _userRepository.GetByIdAsync(customer.UserId);
            if (user != null)
                await _userRepository.DeleteAsync(user);

            await _customerRepository.DeleteAsync(customer);
        }
    }
}
