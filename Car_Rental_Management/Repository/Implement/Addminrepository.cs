using Car_Rental_Management.Data;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Management.Repository.Implement
{
    public class Addminrepository: IAdminrepository

    {
        private readonly ApplicationDbcontext _context;

        public Addminrepository(ApplicationDbcontext context)
        {
            _context = context;
        }

        public async Task AddAdminAsync(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
            //rfgtggyhujmnKVKC
        }
        public async Task<Admin?> GetByIdAsync(Guid id)
        {
            return await _context.Admins
                                 .Include(a => a.User) // include related User
                                 .FirstOrDefaultAsync(a => a.Id == id);
        }

       
        public async Task<List<Admin>> GetAllAsync()
        {
            return await _context.Admins
                                 .Include(a => a.User) // join User table
                                 .ToListAsync();
        }
        public async Task DeleteAdminAsync(Guid id)
        {
            var admin = await _context.Admins
                                      .Include(a => a.User) // join related User
                                      .FirstOrDefaultAsync(a => a.Id == id);

            _context.Users.Remove(admin.User);  // delete related User first
            _context.Admins.Remove(admin);      // delete Admin
            await _context.SaveChangesAsync();
             
            
            
        }
        public async Task UpdateAdminAsync(Admin admin)
        {
            // Update Admin table
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();

            

            
        }

    }
}
