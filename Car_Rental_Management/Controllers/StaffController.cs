
using Car_Rental_Management.Intrface;
using Car_Rental_Management.Mapper;
using Car_Rental_Management.viewmodel;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Car_Rental_Management.Controllers
{
    public class StaffController : Controller
    {
        private readonly IStaffservice _staffService;

        public StaffController(IStaffservice staffService)
        {
            _staffService = staffService;
        }

        // 🔹 List all staff
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var staffList = await _staffService.GetAllStaffAsync();
            return View(staffList); // List<StaffDto>
        }

        // 🔹 View staff details
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var staffDto = await _staffService.GetStaffByIdAsync(id);
            if (staffDto == null)
                return NotFound();

            return View(staffDto); // View: Details.cshtml
        }

        // 🔹 Create staff (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Staffviewmodel()); // Empty form
        }

        // 🔹 Create staff (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Staffviewmodel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                await _staffService.AddStaffAsync(vm);
                TempData["Success"] = "Staff added successfully!";
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred.");
                return View(vm);
            }
        }

        // 🔹 Edit staff (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null)
                return NotFound();

            var vm = Staffmapper.ToViewModel(staff);
            return View(vm);
        }

        // 🔹 Edit staff (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Staffviewmodel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                await _staffService.UpdateStaffAsync(id, vm);
                TempData["Success"] = "Staff updated successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Failed to update staff.");
                return View(vm);
            }
        }

        // 🔹 Delete staff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _staffService.DeleteStaffAsync(id);
                TempData["Success"] = "Staff deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to delete staff.";
                return RedirectToAction("Index");
            }
        }
    }
}
