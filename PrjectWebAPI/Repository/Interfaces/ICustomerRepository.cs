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
        Recipient FindById(int id);
        bool FindByStkAndUserId(RecipientInput recipientInput);
        bool AddRecipient(RecipientInput recipientInput);
        bool DeleteRecipient(int id);
    }
}
