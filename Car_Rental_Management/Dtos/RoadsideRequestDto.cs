namespace Car_Rental_Management.Dtos
{
    public class RoadsideRequestDto
    {
        public Guid RequestId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string CarNumber { get; set; } = string.Empty;

        public string CarModel { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Notes { get; set; } = string.Empty;

        public DateTime RequestDate { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
