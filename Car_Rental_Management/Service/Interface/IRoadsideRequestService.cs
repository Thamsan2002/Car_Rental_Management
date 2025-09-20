using Car_Rental_Management.ViewModel;
using Car_Rental_Management.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Rental_Management.Service.Interface
{
    public interface IRoadsideRequestService
    {
        Task SubmitRequestAsync(RoadsideRequestViewModel model);
        Task<List<RoadsideRequestDto>> GetPendingRequestsAsync();
        Task MarkCompletedAsync(Guid requestId);
    }
}
