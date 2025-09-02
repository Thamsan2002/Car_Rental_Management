using Car_Rental_Management.Intrface;
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

        // GET: Staff/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Staffviewmodel()); // Show empty form
        }

        // POST: Staff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Staffviewmodel vm)
        {
            if (!ModelState.IsValid)
                return View(vm); // Show form if client-side validation fails

            try
            {
                // Service ku ViewModel pass → DB save
                await _staffService.AddStaffAsync(vm);

                TempData["Success"] = "Staff added successfully!";
                return RedirectToAction("Index"); // Redirect to staff list
            }
            catch (ArgumentException ex)
            {
                // Validation errors like empty email/password or invalid format
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            catch (InvalidOperationException ex)
            {
                // Duplicate email
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            catch (Exception)
            {
                // Other unexpected errors
                ModelState.AddModelError(string.Empty, "Something went wrong. Please try again.");
                return View(vm);
            }
        }
    }
}
