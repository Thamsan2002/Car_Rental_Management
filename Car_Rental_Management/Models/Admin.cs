namespace Car_Rental_Management.Models
{
    public class Admin
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        // user foregin key 
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
