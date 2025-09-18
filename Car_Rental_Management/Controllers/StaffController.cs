using Car_Rental_Management.Mapper;
using Car_Rental_Management.Service.Interface;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var staffList = await _staffService.GetAllStaffAsync();
            return View(staffList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var staffDto = await _staffService.GetStaffByIdAsync(id);
            if (staffDto == null) return NotFound();

            return View(staffDto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Staffviewmodel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Staffviewmodel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _staffService.AddStaffAsync(vm);
                TempData["Success"] = "Staff added successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var staffDto = await _staffService.GetStaffByIdAsync(id);
            if (staffDto == null) return NotFound();

            var vm = Staffmapper.ToViewModel(staffDto);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Staffviewmodel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _staffService.UpdateStaffAsync(id, vm);
                TempData["Success"] = "Staff updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _staffService.DeleteStaffAsync(id);
                TempData["Success"] = "Staff deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
