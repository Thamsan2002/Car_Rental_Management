namespace Car_Rental_Management.Models
{
    public class User
    {
        public Guid Id { get; set; }   // <-- Add this
        public string EmailAddress { get; set; }
        public string Password { get; set; }        
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; }

    }
}
