using Car_Rental_Management.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class AdminLoginController : Controller
    {
        private readonly IAdminLoginService _adminLoginService;
        public AdminLoginController(IAdminLoginService adminlogService)
        {
            _adminLoginService = adminlogService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string emailOrPhone, string password)
        {
            // Service call to verify login
            bool isValid = await _adminLoginService.VerifyAdminLoginAsync(emailOrPhone, password);

            if (isValid)
            {
                // ✅ Login success → redirect to existing Admin Dashboard
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                // ❌ Login fail → send error message to front-end
                TempData["Error"] = "Invalid email/phone or password, or you are not an admin.";
                return View();
            }
        }
    }
}
