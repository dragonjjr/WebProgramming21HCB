using Common;

namespace Service.Interfaces
{
    public interface ICustomerService
    {
        bool AddRecipient(RecipientInput recipientInput);
        bool DeleteRecipient(int id);
        UserBalance GetUserBalance(int id);
    }
}
