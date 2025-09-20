using Car_Rental_Management.ViewModel;

namespace Car_Rental_Management.Service.Interface
{
    public interface IContactService
    {
        bool SendMessage(ContactFormViewModel model, out string response);
    }
}
