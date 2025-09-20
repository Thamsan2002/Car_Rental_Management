using Car_Rental_Management.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Car_Rental_Management.Repository.Interface
{
    public interface ICarMaintenanceRepository
    {
        Task AddMaintenanceAsync(CarMaintenance maintenance);
        Task<CarMaintenance> GetMaintenanceByIdAsync(Guid id);
        Task DeleteMaintenanceAsync(Guid id);
        Task<IEnumerable<CarMaintenance>> GetAllAsync();
        Task UpdateMaintenanceAsync(CarMaintenance maintenance);
    }
}
