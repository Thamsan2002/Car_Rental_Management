using Car_Rental_Management.Dtos;
using Car_Rental_Management.Repository.Interface;
using Car_Rental_Management.Service.Interface;
using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Implement
{
    public class ContactService:IContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(IContactRepository repository)
        {
            _repository = repository;
        }


        public IEnumerable<ContactMessageDto> GetAllMessages()
        {
            return _repository.GetAllMessages();
        }

        public bool SendMessage(ContactFormViewModel model, out string response)
        {
            // Duplicate check
            if (_repository.IsDuplicate(model.Email, model.Message))
            {
                response = "Message already sent by this email/message.";
                return false;
            }

            // Mapping ViewModel → DTO
            var dto = new ContactMessageDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Message = model.Message,
                CreatedAt = DateTime.Now
            };

            // DB save via Repository
            _repository.AddMessage(dto);

            response = "Message sent successfully!";
            return true;
        }
    }
}
