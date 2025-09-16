using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Mapper
{
    public class BookingMapper
    {
        public static Booking ToModel(BookingViewmodel dto)
        {
            return new Booking
            {
                Id = Guid.NewGuid(),
                CustomerId = dto.CustomerId,
                CarId = dto.CarId,
                BookingType = dto.BookingType,
                DriverId = dto.DriverId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                TotalPrice = dto.TotalPrice
            };
        }

        public static BookingViewmodel ToDto(Booking model)
        {
            return new BookingViewmodel
            {
                CustomerId = model.CustomerId,
                CarId = model.CarId,
                BookingType = model.BookingType,
                DriverId = model.DriverId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                TotalPrice = model.TotalPrice
            };
        }
    }
}
