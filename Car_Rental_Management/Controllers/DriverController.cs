using Car_Rental_Management.Mapper;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class DriverController : Controller
    {

        private readonly IDriverService _service;

        public DriverController(IDriverService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult CreateDriver()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateDriver(DriverViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            await _service.CreateDriverAsync(viewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var drivers = await _service.GetAllDriversAsync();
            return View(drivers);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var driver = await _service.GetDriverByIdAsync(id);
            if (driver == null) return NotFound();

            var vm = new DriverViewModel
            {
                Id = driver.Id,
                Name = driver.Name,
                PhoneNumber = driver.PhoneNumber,
                EmergencyContact = driver.EmergencyContact,
                Nic = driver.Nic,
                Gender = driver.Gender,
                Address = driver.Address,
                LicenseNumber = driver.LicenseNumber,
                LicenseExpiryDate = driver.LicenseExpiryDate,
                Experience = driver.Experience,
                VehicleType = driver.VehicleType,
                Email = driver.Email
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DriverViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var result = await _service.UpdateDriverAsync(viewModel);
            TempData["Message"] = result;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteDriverAsync(id);
            TempData["Message"] = result;
            return RedirectToAction("Index");
        }


    }
}
