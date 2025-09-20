using Car_Rental_Management.Dtos;

namespace Car_Rental_Management.Repository.Interface
{
    public interface IContactRepository
    {
        void AddMessage(ContactMessageDto dto);
        bool IsDuplicate(string email, string message);
        IEnumerable<ContactMessageDto> GetAllMessages();
    }
}
