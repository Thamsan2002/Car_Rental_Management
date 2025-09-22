using Car_Rental_Management.Mapper;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Car_Rental_Management.Controllers
{
    public class DriverController : Controller
    {
        private readonly IDriverService _service;

        public DriverController(IDriverService service)
        {
            _service = service;
        }

        // ---------------- CREATE DRIVER ----------------
        [HttpGet]
        public IActionResult CreateDriver()
        {
            return View(new DriverViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDriver(DriverViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                await _service.CreateDriverAsync(viewModel);
                TempData["Success"] = "Driver created successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(viewModel);
            }
        }

        // ---------------- LIST DRIVERS ----------------
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var drivers = await _service.GetAllDriversAsync();
            return View(drivers);
        }

        // ---------------- EDIT DRIVER ----------------
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var driver = await _service.GetDriverByIdAsync(id);
            if (driver == null) return NotFound();

            var vm = DriverMapper.ToViewModel(driver);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DriverViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            try
            {
                var result = await _service.UpdateDriverAsync(viewModel);
                TempData["Success"] = result;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(viewModel);
            }
        }

        // ---------------- DELETE DRIVER ----------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _service.DeleteDriverAsync(id);
                TempData["Success"] = result;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
