using Car_Rental_Management.Data;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car_Rental_Management.Repository
{
    public class RoadsideRequestRepository : IRoadsideRequestRepository
    {
        private readonly ApplicationDbcontext _context;

        public RoadsideRequestRepository(ApplicationDbcontext context)
        {
            _context = context;
        }

        // Add new roadside request
        public async Task AddAsync(RoadsideRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            await _context.RoadsideRequests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        // Get all pending requests with customer & car details
        public async Task<List<RoadsideRequest>> GetAllPendingAsync()
        {
            return await _context.RoadsideRequests
                .Include(r => r.Customer) // include navigation property
                .Include(r => r.Car)      // include Car
                .Where(r => r.Status == "Pending")
                .ToListAsync();
        }

        // Get a single request by ID
        public async Task<RoadsideRequest?> GetByIdAsync(Guid requestId)
        {
            return await _context.RoadsideRequests
                .Include(r => r.Customer)
                .Include(r => r.Car)
                .FirstOrDefaultAsync(r => r.RequestId == requestId);
        }

        // Update status
        public async Task UpdateStatusAsync(Guid requestId, string status)
        {
            var req = await _context.RoadsideRequests.FindAsync(requestId);
            if (req != null)
            {
                req.Status = status;
                await _context.SaveChangesAsync();
            }
        }

    }
}
