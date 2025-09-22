using Car_Rental_Management.Dtos;
using Car_Rental_Management.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Rental_Management.Service.Interface
{
    public interface ICarMaintenanceService
    {
        Task<IEnumerable<CarDto>> GetAllAvailableCarsAsync();
        Task AddMaintenanceAsync(CarMaintenanceViewmodel vm);
        Task ReturnCarAsync(Guid maintenanceId);
        Task DeleteMaintenanceAsync(Guid maintenanceId);
        Task<IEnumerable<CarMaintenanceDto>> GetAllMaintenancesAsync();
    }
}
