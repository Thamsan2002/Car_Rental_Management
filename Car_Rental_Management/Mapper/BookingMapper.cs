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
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    TotalPrice = dto.TotalPrice
                };
            }

            //public static BookingViewmodel ToDto(Booking model)
            //{
            //    return new BookingViewmodel
            //    {
            //        CustomerId = model.CustomerId,
            //        CarId = model.CarId,
            //        BookingType = model.BookingType,
            //        StartDate = model.StartDate,
            //        EndDate = model.EndDate,
            //        TotalPrice = model.TotalPrice,
            //        CarMake = model.car?.Make,
            //        CarModel = model.car?.Model,
            //        CarColor = model.car?.Color,
            //        CarPricePerDay = model.car?.PricePerDay ?? 0,
            //        CarImage = model.car?.ImagePaths?.FirstOrDefault() ?? "/uploads/images/noimage.jpg"
            //    };
            //}
    }
}
