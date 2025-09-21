using Car_Rental_Management.Data;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Rental_Management.Repository.Implement
{
    public class RoadsideRequestRepository : IRoadsideRequestRepository
    {
        private readonly ApplicationDbcontext _context;

        public RoadsideRequestRepository(ApplicationDbcontext context)
        {
            _context = context;
        }

        public async Task AddAsync(RoadsideRequest request)
        {
            _context.RoadsideRequests.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RoadsideRequest>> GetAllPendingAsync()
        {
            return await _context.RoadsideRequests
                .Include(r => r.Customer)
                .Include(r => r.Car)
                .Where(r => r.Status == "Pending")
                .ToListAsync();
        }

        public async Task UpdateStatusAsync(Guid requestId, string status)
        {
            var request = await _context.RoadsideRequests.FindAsync(requestId);
            if (request != null)
            {
                request.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RoadsideRequest?> GetByIdAsync(Guid requestId)
        {
            return await _context.RoadsideRequests
                .Include(r => r.Customer)
                .Include(r => r.Car)
                .FirstOrDefaultAsync(r => r.Id == requestId);
        }
    }
}
