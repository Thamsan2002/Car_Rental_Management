using Car_Rental_Management.Models;

namespace Car_Rental_Management.Dtos
{
    public class StaffDto
    {
        public Guid Id { get; set; }
        public string StaffCode { get; set; }
        public string Name { get; set; }
        public StaffStatus Status { get; set; }
    }


}
