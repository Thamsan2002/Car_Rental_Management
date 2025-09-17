using Car_Rental_Management.Data;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Management.Repository.Implement
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbcontext _context;

        public CustomerRepository(ApplicationDbcontext context)
        {
            _context = context;
        }
        public async Task<Customer?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        //// Get single customer by Id
        //public async Task<Customer?> GetByIdAsync(Guid id)
        //{
        //    return await _context.Customers
        //                         .Include(c => c.User)
        //                         .FirstOrDefaultAsync(c => c.Id == id);
        //}

        //// Get all customers
        //public async Task<IEnumerable<Customer>> GetAllAsync()
        //{
        //    return await _context.Customers
        //                         .Include(c => c.User)
        //                         .ToListAsync();
        //}

        // Update existing customer

        //public async Task<Customer> GetByIdAsync(Guid customerId)
        //{
        //    return await _context.Customers
        //                         .FirstOrDefaultAsync(c => c.Id == customerId);
        //}

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        // Delete customer
        public async Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsCustomerExistsAsync(string nic, string drivingLicense, string phone)
        {
            return await _context.Customers.AnyAsync(c =>
                c.NationalIdentityCard == nic ||
                c.DrivingLicenseNumber == drivingLicense ||
                c.Phonenumber == phone);
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public async Task<int> GetCustomerCountAsync()
        {
            return await _context.Customers.CountAsync();
        }

    }
}
