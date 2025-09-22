using Car_Rental_Management.Data;
using Car_Rental_Management.Dtos;
using Car_Rental_Management.Models;
using Car_Rental_Management.Repository.Interface;

namespace Car_Rental_Management.Repository.Implement
{
    public class ContactRepository:IContactRepository
    {
        private readonly ApplicationDbcontext _context;

        public ContactRepository(ApplicationDbcontext context)
        {
            _context = context;
        }

        public void AddMessage(ContactMessageDto dto)
        {
            var entity = new Contact
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Message = dto.Message,
                CreatedAt = dto.CreatedAt
            };

            _context.Contacts.Add(entity);
            _context.SaveChanges();
        }

        public bool IsDuplicate(string email, string message)
        {
            return _context.Contacts.Any(x => x.Email == email && x.Message == message);
        }

        public IEnumerable<ContactMessageDto> GetAllMessages()
        {
            return _context.Contacts.Select(x => new ContactMessageDto
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Phone = x.Phone,
                Message = x.Message,
                CreatedAt = x.CreatedAt
            }).ToList();
        }
    }
}

