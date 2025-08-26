using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Areas.Admin.Controller
{
    public class DriverController : Controllers
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
