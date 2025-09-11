namespace Car_Rental_Management.DTO
{
    public class ReviewDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
