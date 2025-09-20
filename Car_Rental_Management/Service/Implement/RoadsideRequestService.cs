using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;
using Car_Rental_Management.Dtos;
using System.Net.Mail;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Car_Rental_Management.Service.Implement
{
    public class RoadsideRequestService : IRoadsideRequestService
    {
        private readonly IRoadsideRequestRepository _repo;
        private readonly ICarRepository _carRepo;

        public RoadsideRequestService(IRoadsideRequestRepository repo, ICarRepository carRepo)
        {
            _repo = repo;
            _carRepo = carRepo;
        }

        public async Task SubmitRequestAsync(RoadsideRequestViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            // ✅ Find car by number and make
            var car = await _carRepo.GetByNumberOrMakeAsync(model.CarNumber, model.CarModel);
            if (car == null)
            {
                throw new Exception("Car not found! Please check number and make.");
            }

            var request = new RoadsideRequest
            {
                Id = Guid.NewGuid(),
                CustomerId = model.CustomerId,
                CarId = car.Id,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Notes = model.Notes ?? "",
                Status = "Pending",
                RequestDate = DateTime.Now
            };

            await _repo.AddAsync(request);

            // ✅ Send admin alerts
            SendAdminEmail(request, car);
            await SendAdminWhatsAppAsync(request, car);
        }

        public async Task<List<RoadsideRequestDto>> GetPendingRequestsAsync()
        {
            var requests = await _repo.GetAllPendingAsync();
            return requests.Select(r => new RoadsideRequestDto
            {
                RequestId = r.Id,
                CustomerName = r.Customer != null ? r.Customer.FullName : "Unknown",
                CarNumber = r.Car != null ? r.Car.PlateNumber : "N/A",
                CarModel = r.Car != null ? r.Car.Model : "N/A",
                Latitude = r.Latitude,
                Longitude = r.Longitude,
                Notes = r.Notes,
                RequestDate = r.RequestDate,
                Status = r.Status
            }).ToList();
        }

        public async Task MarkCompletedAsync(Guid requestId)
        {
            await _repo.UpdateStatusAsync(requestId, "Completed");
        }

        // ---------------- Admin Alerts ----------------
        private void SendAdminEmail(RoadsideRequest request, Car car)
        {
            try
            {
                var smtp = new SmtpClient("smtp.yourmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("admin@gmail.com", "password"),
                    EnableSsl = true
                };

                var message = new MailMessage
                {
                    From = new MailAddress("admin@gmail.com"),
                    Subject = $"New Roadside Request: {request.Id}",
                    Body = $"Customer: {request.Customer?.FullName}\nCar: {car.PlateNumber} ({car.Model})\nLocation: {request.Latitude},{request.Longitude}\nNotes: {request.Notes}",
                    IsBodyHtml = false
                };
                message.To.Add("admin@gmail.com");
                smtp.Send(message);
            }
            catch
            {
                // log error
            }
        }

        private async Task SendAdminWhatsAppAsync(RoadsideRequest request, Car car)
        {
            try
            {
                using var client = new HttpClient();
                var data = new
                {
                    to = "whatsapp:+94772671337",
                    message = $"New Roadside Request!\nCustomer: {request.Customer?.FullName}\nCar: {car.PlateNumber} ({car.Model})\nLocation: {request.Latitude},{request.Longitude}\nNotes: {request.Notes}"
                };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                await client.PostAsync("https://api.whatsapp.com/send", content);
            }
            catch
            {
                // log error
            }
        }
    }
}
