using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            ViewData["Layout"] = "_AdminLayout";
            return View();
        }
    }
}
