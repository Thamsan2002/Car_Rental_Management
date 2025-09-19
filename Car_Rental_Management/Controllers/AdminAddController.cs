using Car_Rental_Management.Dtos;
using Car_Rental_Management.Mapper;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car_Rental_Management.Controllers
{
    public class AdminAddController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUserRepository _userRepo; // for password hashing

        public AdminAddController(IAdminService adminService, IUserRepository userRepo)
        {
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }

        // GET: Admin/Add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // POST: Admin/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Adminviewmodel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            // Check if user exists
            bool exists = await _userRepo.IsEmailOrPhoneExistAsync(vm.Email, vm.PhoneNumber);
            if (exists)
            {
                ModelState.AddModelError("", "User with this email or phone already exists.");
                return View(vm);
            }

            // Hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(vm.Password);
            vm.Password = hashedPassword; // store hashed password in ViewModel temporarily

            // Call service to save admin + user
            await _adminService.AddAdminAsync(vm);

            TempData["SuccessMessage"] = "Admin saved successfully.";
            return RedirectToAction("Add");
        }

        // GET: Admin/List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<AdminDto> adminList = await _adminService.GetAllAdminsAsync();
            return View(adminList);
        }

        // GET: Admin/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var adminVm = await _adminService.GetAdminByIdAsync(id);
            if (adminVm == null)
                return NotFound();

            var vm = AdminMapper.ToViewModel(adminVm);
            return View(vm);
        }

        // POST: Admin/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Adminviewmodel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            // Hash password if it's updated
            if (!string.IsNullOrEmpty(vm.Password))
                vm.Password = BCrypt.Net.BCrypt.HashPassword(vm.Password);

            await _adminService.UpdateAdminAsync(id, vm);

            TempData["Success"] = "Admin updated successfully.";
            return RedirectToAction("List");
        }

        // POST: Admin/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _adminService.DeleteAdminAsync(id);
            TempData["Success"] = "Admin deleted successfully.";
            return RedirectToAction("List");
        }
    }
}
