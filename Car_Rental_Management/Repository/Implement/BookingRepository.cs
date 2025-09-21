using Car_Rental_Management.Data;
using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Management.Repository.Implement
{
    public class BookingRepository:IBookingRepository
    {
        private readonly ApplicationDbcontext _context;

        public BookingRepository(ApplicationDbcontext context)
        {
            _context = context;
        }

        public async Task<List<BookingDateRangeDto>> GetBookingDateRangesByCarAsync(Guid carId)
        {
            return await _context.Bookings
                .Where(b => b.CarId == carId)
                .Select(b => new BookingDateRangeDto
                {
                    Start = b.StartDate.ToString("yyyy-MM-dd"),
                    End = b.EndDate.ToString("yyyy-MM-dd")
                })
                .ToListAsync();
        }

        public async Task CreateAsync(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking), "Booking cannot be null.");

            try
            {
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                // Log more details for DB issues
                Console.WriteLine($"Database error: {dbEx.Message}");
                Console.WriteLine($"Inner Exception: {dbEx.InnerException?.Message}");
                throw new InvalidOperationException("Error saving booking to the database.", dbEx);
            }
            catch (Exception ex)
            {
                // General exception handler
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception("An unexpected error occurred while creating the booking.", ex);
            }
        }

        //public async Task AddAsync(Booking booking)
        //{
        //    _context.Bookings.Add(booking);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<List<Booking>> GetAllAsync()
        //{
        //    return await _context.Bookings
        //        .Include(b => b.Customer)
        //        .Include(b => b.car)
        //        .ToListAsync();
        //}

        //public async Task<Booking> GetByIdAsync(Guid id)
        //{
        //    return await _context.Bookings
        //        .Include(b => b.Customer)
        //        .Include(b => b.car)
        //        .FirstOrDefaultAsync(b => b.Id == id);
        //}

        //public async Task<List<Booking>> GetByCustomerIdAsync(Guid customerId)
        //{
        //    return await _context.Bookings
        //        .Include(b => b.Customer)
        //        .Include(b => b.car)
        //        .Where(b => b.CustomerId == customerId)
        //        .ToListAsync();
        //}
    }
}
