using Car_Rental_Management.Data;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;

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
    }
}
