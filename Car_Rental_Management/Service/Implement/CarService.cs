using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Car_Rental_Management.Service.Implement
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IWebHostEnvironment _env;

        public CarService(ICarRepository carRepository, IWebHostEnvironment env)
        {
            _carRepository = carRepository;
            _env = env;
        }

        public async Task<bool> AddCarAsync(CarViewModel model)
        {
            try
            {
                // 1. Upload Images
                var imagePaths = new List<string>();

                if (model.Images != null && model.Images.Count > 0)
                {
                    string imageFolder = Path.Combine(_env.WebRootPath, "uploads", "images");
                    Directory.CreateDirectory(imageFolder);

                    foreach (var image in model.Images)
                    {
                        if (image.Length > 0)
                        {
                            var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                            var filePath = Path.Combine(imageFolder, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }

                            imagePaths.Add("/uploads/images/" + fileName);
                        }
                    }
                }

                // 3. Map ViewModel to Domain Model
                var car = new Car
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
                    IsAvailable = model.Available, // ✅ Already a bool now
                    Color = model.Color,
                    Description = model.Description,
                    ImagePaths = imagePaths,
                    CreatedAt = DateTime.UtcNow
                };


                // 4. Save to DB via repository
                await _carRepository.AddAsync(car);

                return true;
            }
            catch (Exception ex)
            {
                // TODO: Log the exception (e.g., to file, DB, or monitoring system)
                return false;
            }
        }
    }
}
