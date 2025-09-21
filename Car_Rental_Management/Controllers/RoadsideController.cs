using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Car_Rental_Management.Controllers
{
    public class RoadsideController : Controller
    {
        private readonly IRoadsideRequestService _service;

        public RoadsideController(IRoadsideRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult RequestHelp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RequestHelp(RoadsideRequestViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _service.SubmitRequestAsync(model);
                TempData["SuccessMessage"] = "Request sent! Admin will contact you soon.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("RequestHelp");
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var requests = await _service.GetPendingRequestsAsync();
            return View(requests);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteRequest(Guid id)
        {
            await _service.MarkCompletedAsync(id);
            return RedirectToAction("Dashboard");
        }
    }
}
