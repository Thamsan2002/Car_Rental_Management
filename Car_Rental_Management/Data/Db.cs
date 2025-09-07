using Car_Rental_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Management.Data
{
    public class Db:DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options)
        {

        }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
