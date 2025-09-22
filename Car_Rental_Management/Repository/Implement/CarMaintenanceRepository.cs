using Car_Rental_Management.Data;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car_Rental_Management.Repository.Implement
{
    public class CarMaintenanceRepository : ICarMaintenanceRepository
    {
        private readonly ApplicationDbcontext _context;

        public CarMaintenanceRepository(ApplicationDbcontext context)
        {
            _context = context;
        }

        public async Task AddMaintenanceAsync(CarMaintenance maintenance)
        {
            await _context.CarMaintenances.AddAsync(maintenance);
            await _context.SaveChangesAsync();
        }

        public async Task<CarMaintenance> GetMaintenanceByIdAsync(Guid id)
        {
            return await _context.CarMaintenances
                .Include(m => m.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task DeleteMaintenanceAsync(Guid id)
        {
            var maintenance = await _context.CarMaintenances.FindAsync(id);
            if (maintenance != null)
            {
                _context.CarMaintenances.Remove(maintenance);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CarMaintenance>> GetAllAsync()
        {
            return await _context.CarMaintenances.Include(m => m.Car).ToListAsync();
        }

        public async Task UpdateMaintenanceAsync(CarMaintenance maintenance)
        {
            _context.CarMaintenances.Update(maintenance);
            await _context.SaveChangesAsync();
        }
    }
}
