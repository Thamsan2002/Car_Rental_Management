using Car_Rental_Management.Models;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IRoadsideRequestRepository
    {
        Task AddAsync(RoadsideRequest request);
        Task<List<RoadsideRequest>> GetAllPendingAsync();
        Task UpdateStatusAsync(Guid requestId, string status);
        Task<RoadsideRequest?> GetByIdAsync(Guid requestId);
    }
}
