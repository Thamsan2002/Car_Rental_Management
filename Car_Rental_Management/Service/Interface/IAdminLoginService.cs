namespace Car_Rental_Management.Service.Interface
{
    public interface IAdminLoginService
    {
        Task<bool> VerifyAdminLoginAsync(string emailOrPhone, string password);
    }
}
