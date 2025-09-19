using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            // Check session for security
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminEmail")))
            {
                return RedirectToAction("Login", "AdminLogin");
            }

            ViewData["Layout"] = "_AdminLayout";
            return View();
        }
    }
}
