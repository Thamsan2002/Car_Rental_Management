using Car_Rental_Management.Dtos;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Interface
{
    public interface IReportService
    {
        Task SubmitRequestAsync(RoadsideRequestViewModel model);

        // Get pending requests (for dashboard, returns DTO with RequestId)
        Task<List<RoadsideRequestDto>> GetPendingRequestsAsync();

        // Mark request as completed using Guid Id
        Task MarkCompletedAsync(Guid requestId);
    }
}
