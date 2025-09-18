namespace Car_Rental_Management.ViewModel
{
    public class UserViewModel
    {
       
        public string Email { get; set; }
        public string PhoneNumber  { get; set; }

        // For password verification
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
