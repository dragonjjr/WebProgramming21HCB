using Common;
using Repository.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Recipient FindRecipientById(int id);
        UserManage FindUserById(int id);
        bool FindRecipientByStkAndUserId(RecipientInput recipientInput);
        bool AddRecipient(RecipientInput recipientInput);
        bool DeleteRecipient(int id);
        UserBalance GetUserBalance(int id);
    }
}
