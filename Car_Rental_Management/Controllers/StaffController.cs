using Car_Rental_Management.Mapper;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
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

            public async Task<IActionResult> Index()
            {
                var staffList = await _staffService.GetAllAsync();
                return View(staffList);
            }

            [HttpGet]
            public IActionResult Create() => View();

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(StaffViewModel vm)
            {
                if (!ModelState.IsValid) return View(vm);

                try
                {
                    await _staffService.AddStaffAsync(vm);
                    TempData["Success"] = "Staff added successfully";
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
                var staff = await _staffService.GetStaffByIdAsync(id);
                if (staff == null) return NotFound();
                return View(staff);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(Guid id, StaffViewModel vm)
            {
                if (!ModelState.IsValid) return View(vm);

                try
                {
                    await _staffService.UpdateStaffAsync(id, vm);
                    TempData["Success"] = "Staff updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Cant Update", ex.Message);
                    return View(vm);
                }
            }

            [HttpPost]
            public async Task<IActionResult> Delete(Guid id)
            {
                try
                {
                    await _staffService.DeleteStaffAsync(id);
                    TempData["Success"] = "Staff deleted successfully";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
                return RedirectToAction(nameof(Index));
            }
        }
    }
