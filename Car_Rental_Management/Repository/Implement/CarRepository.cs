using Car_Rental_Management.Data;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using System;
using System.Runtime.ConstrainedExecution;

namespace Car_Rental_Management.Repository.Implement
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbcontext _context;

        public CarRepository(ApplicationDbcontext context)
        {
            _context = context;
        }

        public async Task AddAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }
    }
}
