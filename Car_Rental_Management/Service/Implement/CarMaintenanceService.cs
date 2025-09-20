using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car_Rental_Management.Service.Implement
{
    public class CarMaintenanceService : ICarMaintenanceService
    {
        private readonly ICarRepository _carRepo;
        private readonly ICarMaintenanceRepository _maintenanceRepo;

        public CarMaintenanceService(ICarRepository carRepo, ICarMaintenanceRepository maintenanceRepo)
        {
            _carRepo = carRepo;
            _maintenanceRepo = maintenanceRepo;
        }

        public async Task<IEnumerable<CarDto>> GetAllAvailableCarsAsync()
        {
            var cars = await _carRepo.GetAvailableCarsAsync();
            return cars.Select(c => new CarDto
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                Year = c.Year,
                Price = c.PricePerDay,
                Available = c.IsAvailable
            });
        }

        public async Task AddMaintenanceAsync(CarMaintenanceViewmodel vm)
        {
            var car = await _carRepo.GetByIdAsync(vm.CarId);
            if (car == null) return;

            car.IsAvailable = false;
            await _carRepo.UpdateAsync(car);

            var maintenance = new CarMaintenance
            {
                Id = Guid.NewGuid(),
                CarId = vm.CarId,
                Description = vm.MaintenanceType,
                StartDate = vm.MaintenanceDate,
                Notes = vm.Notes,
                Cost = vm.Cost,
                IsReturned = false
            };

            await _maintenanceRepo.AddMaintenanceAsync(maintenance);
        }

        public async Task ReturnCarAsync(Guid maintenanceId)
        {
            var maintenance = await _maintenanceRepo.GetMaintenanceByIdAsync(maintenanceId);
            if (maintenance == null) return;

            maintenance.IsReturned = true;
            maintenance.EndDate = DateTime.Now;
            await _maintenanceRepo.UpdateMaintenanceAsync(maintenance);

            var car = await _carRepo.GetByIdAsync(maintenance.CarId);
            if (car != null)
            {
                car.IsAvailable = true;
                await _carRepo.UpdateAsync(car);
            }
        }

        public async Task DeleteMaintenanceAsync(Guid maintenanceId)
        {
            await _maintenanceRepo.DeleteMaintenanceAsync(maintenanceId);
        }

        public async Task<IEnumerable<CarMaintenanceDto>> GetAllMaintenancesAsync()
        {
            var maintenances = await _maintenanceRepo.GetAllAsync();
            return maintenances.Select(m => new CarMaintenanceDto
            {
                MaintenanceId = m.Id,
                CarId = m.CarId,
                CarMake = m.Car.Make,
                CarModel = m.Car.Model,
                Description = m.Description,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                Cost = m.Cost,
                IsReturned = m.IsReturned
            });
        }
    }
}
