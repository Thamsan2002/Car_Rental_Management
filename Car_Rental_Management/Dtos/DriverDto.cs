namespace Car_Rental_Management.Dtos
{
    public class DriverDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmergencyContact { get; set; }
        public string Nic { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string LicenseNumber { get; set; }
        public string LicenseExpiryDate { get; set; }
        public string Experience { get; set; }
        public string VehicleType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
