using Common;
using Repository.DBContext;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Recipient FindRecipientById(int id);
        UserManage FindUserById(int id);
        bool FindRecipientByStkAndUserId(RecipientInput recipientInput);
        bool AddRecipient(RecipientInput recipientInput);
        bool UpdateRecipient(int id, RecipientEdit recipientEdit);
        bool DeleteRecipient(int id);
        UserBalance GetUserBalance(int id);
        List<RecipientOutput> GetListRecipientByUserId(int id);
        List<BankReferenceVM> GetBankReferences();
    }
}
