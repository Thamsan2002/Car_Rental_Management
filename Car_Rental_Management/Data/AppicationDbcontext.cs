using Car_Rental_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Management.Data
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
        {

        }


        public DbSet<Driver> Drivers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CarMaintenance> CarMaintenances { get; set; }
        public DbSet<Contact> Contacts { get; set; }


    }
}
