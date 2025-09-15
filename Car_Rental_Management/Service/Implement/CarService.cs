using Car_Rental_Management.Dtos;
using Car_Rental_Management.Mapper;
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
               
                var car = CarMapper.ToCar(model, imagePaths);


                await _carRepository.AddAsync(car);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<CarDto>> GetAllCarsAsync()
        {
            var cars = await _carRepository.GetAllAsync();
            return cars.Select(c => CarMapper.ToCarDto(c)).ToList();
        }

        public async Task<CarDto?> GetCarByIdAsync(Guid id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null) return null;

            return new CarDto
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                Plate = car.PlateNumber,
                Seats = car.Seats,
                Transmission = car.Transmission,
                Fuel = car.FuelType,
                Mileage = car.Mileage,
                Price = car.PricePerDay,
                Available = car.IsAvailable,
                Color = car.Color,
                Description = car.Description,
                ImageUrls = car.ImagePaths
            };
        }

        public async Task<bool> UpdateCarAsync(CarViewModel model)
        {
            try
            {
                var car = await _carRepository.GetByIdAsync(model.Id);
                if (car == null) return false;

                car.Make = model.Make;
                car.Model = model.Model;
                car.Year = model.Year;
                car.PlateNumber = model.Plate;
                car.Seats = model.Seats;
                car.Transmission = model.Transmission;
                car.FuelType = model.Fuel;
                car.Mileage = model.Mileage;
                car.PricePerDay = model.Price;
                car.IsAvailable = model.Available;
                car.Color = model.Color;
                car.Description = model.Description;

                await _carRepository.UpdateAsync(car);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCarAsync(Guid id)
        {
            try
            {
                var car = await _carRepository.GetByIdAsync(id);
                if (car == null) return false;

                await _carRepository.DeleteAsync(car);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
