using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using System.Net;
using System.Net.Mail;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Car_Rental_Management.Service
{
    public class RoadsideRequestService : IRoadsideRequestService
    {
        private readonly IRoadsideRequestRepository _repo;
        private readonly IConfiguration _config;

        public RoadsideRequestService(IRoadsideRequestRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<bool> CreateRequestAsync(RoadsideRequestDto dto)
        {
            // Mapping DTO → Model
            var entity = new RoadsideRequest
            {
                CustomerId = dto.CustomerId,
                CarId = dto.CarId,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Notes = dto.Notes,
                Status = "Pending",
                RequestDate = DateTime.Now
            };

            // Save to DB
            await _repo.AddAsync(entity);

            // 🔔 Notifications
            await SendEmailNotification(dto);
            await SendWhatsAppNotification(dto);

            return true;
        }

        private async Task SendEmailNotification(RoadsideRequestDto dto)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_config["Smtp:User"], _config["Smtp:Password"]),
                EnableSsl = true,
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_config["Smtp:User"]),
                Subject = "New Roadside Assistance Request",
                Body = $"Customer ID: {dto.CustomerId}\nCar ID: {dto.CarId}\nIssue: {dto.Notes}",
                IsBodyHtml = false,
            };
            mail.To.Add("sureshkirusthiya@gmail.com");

            await smtpClient.SendMailAsync(mail);
        }

        private async Task SendWhatsAppNotification(RoadsideRequestDto dto)
        {
            TwilioClient.Init(_config["Twilio:AccountSid"], _config["Twilio:AuthToken"]);

            await MessageResource.CreateAsync(
                body: $"🚨 New Roadside Request\nCustomer: {dto.CustomerId}\nCar: {dto.CarId}\nIssue: {dto.Notes}",
                from: new PhoneNumber("whatsapp:+946784561"),
                to: new PhoneNumber("whatsapp:+94763891103") // admin number
            );
        }

        public async Task<IEnumerable<RoadsideRequestDto>> GetAllRequestsAsync()
        {
            var data = await _repo.GetAllPendingAsync();
            return data.Select(r => new RoadsideRequestDto
            {
                CustomerId = r.CustomerId,
                CarId = r.CarId,
                Latitude = r.Latitude,
                Longitude = r.Longitude,
                Notes = r.Notes
            }).ToList();
        }

        public async Task MarkResolvedAsync(Guid requestId)
        {
            await _repo.UpdateStatusAsync(requestId, "Resolved");
        }
    }
}
