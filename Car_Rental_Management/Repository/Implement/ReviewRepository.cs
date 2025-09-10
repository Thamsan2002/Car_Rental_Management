using Car_Rental_Management.Data;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repositry;
using Microsoft.EntityFrameworkCore;
using System;

namespace Car_Rental_Management.Repositry
{
    public class ReviewRepository
    {
        private readonly ApplicationDbcontext _context;

        public ReviewRepository(ApplicationDbcontext context)
        {
            _context = context;
        }

        public async Task<Review> AddAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews.Include(r => r.User).ToListAsync();
        }
    }
}
