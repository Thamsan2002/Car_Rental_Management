using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Repositry;
using Car_Rental_Management.viewmodel;

namespace Car_Rental_Management.Services
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepo;
        private readonly IUserRepository _userRepo;

        public ReviewService(ReviewRepository reviewRepo, IUserRepository userRepo)
        {
            _reviewRepo = reviewRepo;
            _userRepo = userRepo;
        }

        public async Task<Review> AddReviewAsync(ReviewViewModel vm)
        {
            var user = await _userRepo.GetByEmailAsync(vm.Email);
            if (user == null)
                throw new Exception("Email not found. Please register first.");

            var review = new Review
            {
                UserId = user.userId,
                Description = vm.Description,
                Rating = vm.Rating,
                CreatedAt = DateTime.UtcNow,
                User = user
            };

            return await _reviewRepo.AddAsync(review);
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _reviewRepo.GetAllAsync();
        }

        public async Task<List<Review>> GetTopReviewsAsync(int minRating = 4, int take = 5)
        {
            var all = await _reviewRepo.GetAllAsync();
            return all.Where(r => r.Rating >= minRating)
                      .OrderByDescending(r => r.CreatedAt)
                      .Take(take)
                      .ToList();
        }
    }
}
