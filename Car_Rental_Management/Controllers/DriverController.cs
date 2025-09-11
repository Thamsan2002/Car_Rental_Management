using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class DriverController : Controller
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        public async Task<IActionResult> Index()
        {
            var drivers = await _driverService.GetAllDriversAsync();
            return View(drivers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DriverViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _driverService.CreateDriverAsync(vm);
                TempData["Success"] = "Driver added successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var driver = await _driverService.GetDriverByIdAsync(id);
            if (driver == null) return NotFound();

            return View(new DriverViewModel
            {
                Id = driver.Id,
                Name = driver.Name,
                Email = driver.Email,
                PhoneNumber = driver.PhoneNumber,
                EmergencyContact = driver.EmergencyContact,
                Nic = driver.Nic,
                Gender = driver.Gender,
                Address = driver.Address,
                LicenseNumber = driver.LicenseNumber,
                LicenseExpiryDate = DateTime.Parse(driver.LicenseExpiryDate),
                Experience = driver.Experience,
                VehicleType = driver.VehicleType,
                Password = "" // Optionally leave blank
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DriverViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _driverService.UpdateDriverAsync(vm);
                TempData["Success"] = "Driver updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _driverService.DeleteDriverAsync(id);
                TempData["Success"] = "Driver deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
