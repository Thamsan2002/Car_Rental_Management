using Car_Rental_Management.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Car_Rental_Management.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

    }
}
