using Car_Rental_Management.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class AdminLoginController : Controller
    {
        private readonly IAdminLoginService _adminLoginService;

        public AdminLoginController(IAdminLoginService adminLoginService)
        {
            _adminLoginService = adminLoginService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string emailOrPhone, string password)
        {
            var user = await _adminLoginService.VerifyAdminLoginAsync(emailOrPhone, password);

            if (user != null)
            {
                // Store user info in session
                HttpContext.Session.SetString("AdminEmail", user.Email);
                HttpContext.Session.SetString("AdminRole", user.Role);

                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                TempData["Error"] = "Invalid email/phone or password, or you are not an admin.";
                return View();
            }
        }
    }
}
