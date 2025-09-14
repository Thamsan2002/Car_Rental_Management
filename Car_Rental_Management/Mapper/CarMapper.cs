using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Mapper
{
    public class CarMapper
    {
        public static Car ToCar(CarViewModel model, List<string> imagePaths)
        {
            return new Car
            {
                Make = model.Make,
                Model = model.Model,
                Year = model.Year,
                PlateNumber = model.Plate,
                Seats = model.Seats,
                Transmission = model.Transmission,
                FuelType = model.Fuel,
                Mileage = model.Mileage,
                PricePerDay = model.Price,
                IsAvailable = model.Available,
                Color = model.Color,
                Description = model.Description,
                ImagePaths = imagePaths,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static CarDto ToCarDto(Car c)
        {
            return new CarDto
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                Year = c.Year,
                Plate = c.PlateNumber,
                Seats = c.Seats,
                Transmission = c.Transmission,
                Fuel = c.FuelType,
                Mileage = c.Mileage,
                Price = c.PricePerDay,
                Available = c.IsAvailable,
                Color = c.Color,
                Description = c.Description,
                ImageUrls = c.ImagePaths
            };
        }
    }
}
