using Car_Rental_Management.Data;
using Car_Rental_Management.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Car_Rental_Management.Extra_Needs
{
    public class CreateSuperAdmin
    {
        private readonly ApplicationDbcontext _context;

        public CreateSuperAdmin(ApplicationDbcontext context)
        {
            _context = context;
        }

        public async Task EnsureSuperAdminAsync()
        {
            bool exists = await _context.Users.AnyAsync(u => u.Role == "Superadmin");

            if (!exists)
            {
                var superAdmin = new User
                {
                    Email = "admin@gmail.com",
                    Role = "Superadmin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                    PhoneNumber = "0762671337"
                };

                _context.Users.Add(superAdmin);
                await _context.SaveChangesAsync();
            }
        }
    }
}
