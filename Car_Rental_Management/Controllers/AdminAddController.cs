using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class AdminAddController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminAddController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // GET: Admin/Add
        public IActionResult Add()
        {
            return View(); // Returns Add.cshtml Razor form
        }

        // POST: Admin/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Adminviewmodel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm); // Validation failed → return form
            }

            try
            {
                // Call service to save admin + user
                await _adminService.AddAdminAsync(vm);

                TempData["SuccessMessage"] = "Admin saved successfully.";

                // After add, return same add form (no list page)
                return RedirectToAction("Add");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message); // duplicate error
                return View(vm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong: " + ex.Message);
                return View(vm);
            }
        }
    }
}

