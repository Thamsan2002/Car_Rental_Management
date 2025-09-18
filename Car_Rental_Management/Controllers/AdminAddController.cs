using Car_Rental_Management.Dtos;
using Car_Rental_Management.Mapper;
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

            // Call service to save admin + user
            await _adminService.AddAdminAsync(vm);

            // Optionally, TempData message can be used in view
            TempData["SuccessMessage"] = "Admin saved successfully.";

            // After add, return same Add form
            return RedirectToAction("Add");
        }

        public async Task<IActionResult> List()
        {
            // Fetch list of admins as DTO
            List<AdminDto> adminList = await _adminService.GetAllAdminsAsync();
            return View(adminList); // pass DTO list to Razor view
        }
        // GET: Admin/Edit/{id} → show prefilled form
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var adminVm = await _adminService.GetAdminByIdAsync(id); // returns AdminViewModel
            if (adminVm == null)
                return NotFound();
            var vm = AdminMapper.ToViewModel(adminVm);

            return View(vm);
        }

        // POST: Admin/Edit/{id} → handle update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Adminviewmodel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            await _adminService.UpdateAdminAsync(id, vm); // service handles mapping + repo

            TempData["Success"] = "Admin updated successfully.";
            return RedirectToAction("List");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _adminService.DeleteAdminAsync(id);
            return RedirectToAction("List");
        }

    }
}

