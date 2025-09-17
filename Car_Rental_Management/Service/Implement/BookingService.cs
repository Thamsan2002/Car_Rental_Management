using Car_Rental_Management.Dtos;
using Car_Rental_Management.Mapper;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Implement
{
    public class BookingService:IBookingService
    {
        private readonly IBookingRepository _repo;
        private readonly ICarRepository _carRepo;
        private readonly IDriverRepository _driverRepo;

        public BookingService(IBookingRepository repo, ICarRepository carRepo, IDriverRepository driverRepo)
        {
            _repo = repo;
            _carRepo = carRepo;
            _driverRepo = driverRepo;
        }

        public async Task CreateBookingAsync(BookingViewmodel dto)
        {
            if (dto.CustomerId == Guid.Empty) throw new Exception("Customer ID missing");
            if (dto.CarId == Guid.Empty) throw new Exception("Car ID missing");
            if (dto.StartDate >= dto.EndDate) throw new Exception("Invalid date range");

            var model = BookingMapper.ToModel(dto);
            await _repo.CreateAsync(model);
        }

        public async Task<BookingViewmodel> GetBookingDtoByIdAsync(Guid id)
        {
            var booking = await _repo.GetByIdAsync(id);
            if (booking == null) return null;

            var dto = BookingMapper.ToDto(booking);

            var car = await _carRepo.GetByIdAsync(booking.CarId);
            if (car != null)
            {
                dto.CarMake = car.Make;
                dto.CarModel = car.Model;
                dto.CarColor = car.Color;
                dto.CarPricePerDay = car.PricePerDay ?? 0;
                dto.CarImage = car.ImagePaths?.FirstOrDefault() ?? "/uploads/images/noimage.jpg";
            }

            return dto;
        }

        public async Task<List<BookingViewmodel>> GetAllBookingsAsync()
        {
            var bookings = await _repo.GetAllAsync();
            return bookings.Select(BookingMapper.ToDto).ToList();
        }

        public async Task<List<BookingViewmodel>> GetBookingsByCustomerIdAsync(Guid customerId)
        {
            var bookings = await _repo.GetByCustomerIdAsync(customerId);
            return bookings.Select(BookingMapper.ToDto).ToList();
        }
    }
}
