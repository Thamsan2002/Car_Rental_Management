using Car_Rental_Management.Dtos;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace Car_Rental_Management.Controllers
{
    public class CarMaintenanceController : Controller
    {
        private readonly ICarMaintenanceService _service;

        public CarMaintenanceController(ICarMaintenanceService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _service.GetAllAvailableCarsAsync();
            var vm = new CarMaintenanceViewmodel
            {
                AvailableCars = cars
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddMaintenance(CarMaintenanceViewmodel vm)
        {
            //if (!ModelState.IsValid)
            //{
            //    vm.AvailableCars = await _service.GetAllAvailableCarsAsync();
            //    return View("Index", vm);
            //}

            await _service.AddMaintenanceAsync(vm);
            return RedirectToAction("History");
        }

        [HttpPost]
        public async Task<IActionResult> ReturnCar(Guid maintenanceId)
        {
            await _service.ReturnCarAsync(maintenanceId);
            return RedirectToAction("History");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMaintenance(Guid maintenanceId)
        {
            await _service.DeleteMaintenanceAsync(maintenanceId);
            return RedirectToAction("History");
        }

        // History page: show all maintenance records
        public async Task<IActionResult> History()
        {
            var maintenances = await _service.GetAllMaintenancesAsync();
            return View(maintenances); // History.cshtml view
        }
    }
}
