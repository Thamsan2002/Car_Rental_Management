namespace Car_Rental_Management.Dtos
{
    public class RoadsideRequestDto
    {
        public Guid CustomerId { get; set; }
        public Guid CarId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Notes { get; set; }
    }
}
