using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Rental_Management.Service.Interface
{
    public interface IRoadsideRequestService
    {

        Task<bool> CreateRequestAsync(RoadsideRequestDto dto);
        Task<IEnumerable<RoadsideRequestDto>> GetAllRequestsAsync();
        Task MarkResolvedAsync(Guid requestId);
    }
}
