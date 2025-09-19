using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_contactService.SendMessage(model, out string response))
            {
                ViewBag.SuccessMessage = response;
                ModelState.Clear();
                return View();
            }
            else
            {
                ModelState.AddModelError("", response);
                return View(model);
            }
        }


    }
}
