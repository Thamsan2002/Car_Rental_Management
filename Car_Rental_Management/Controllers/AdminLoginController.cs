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
            bool isValid = await _adminLoginService.VerifyAdminLoginAsync(emailOrPhone, password);

            if (isValid)
            {
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
