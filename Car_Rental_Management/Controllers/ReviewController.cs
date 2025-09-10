using Car_Rental_Management.DTO;
using Car_Rental_Management.Services;
using Car_Rental_Management.viewmodel;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_Management.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public IActionResult Create() => View(new ReviewViewModel());

        [HttpPost]
        public async Task<IActionResult> Create(ReviewViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _reviewService.AddReviewAsync(vm);
                TempData["Success"] = "Thank you for your review!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("register"))
                    return RedirectToAction("Register", "Customer");

                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            //var reviews = await _reviewService.GetAllReviewsAsync();
            //var reviewDTOs = reviews.Select(r => new ReviewDto
            //{
            //    CustomerName = r.User.Name,
            //    Description = r.Description,
            //    Rating = r.Rating,
            //    CreatedAt = r.CreatedAt
            //}).ToList();

            return View();
        }
    }
}
