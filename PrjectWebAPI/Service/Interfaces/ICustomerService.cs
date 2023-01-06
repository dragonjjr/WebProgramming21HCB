using Common;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface ICustomerService
    {
        bool AddRecipient(RecipientInput recipientInput);
        bool UpdateRecipient(int id, RecipientEdit recipientEdit);
        bool DeleteRecipient(int id);
        UserBalance GetUserBalance(int id);
        List<RecipientOutput> GetListRecipientByUserId(int id);
        List<BankReferenceVM> GetBankReferences();
    }
}
