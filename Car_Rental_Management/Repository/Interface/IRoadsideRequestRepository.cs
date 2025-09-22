using Car_Rental_Management.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IRoadsideRequestRepository
    {

      Task AddAsync(RoadsideRequest entity);
        Task<List<RoadsideRequest>> GetAllPendingAsync();  // rename to Pending
        Task<RoadsideRequest?> GetByIdAsync(Guid requestId);
        Task UpdateStatusAsync(Guid requestId, string status);
    }
}
