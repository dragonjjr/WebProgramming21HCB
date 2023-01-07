using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Helpers;

namespace Service.Interfaces
{
    public interface IEmailService
    {
        bool SendMailForAccount(string email);
        bool SendMailForTransaction(string stk, int transactionId);
    }
}
