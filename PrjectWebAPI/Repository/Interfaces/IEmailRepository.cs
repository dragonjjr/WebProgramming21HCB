using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Helpers;

namespace Repository.Interfaces
{
    public interface IEmailRepository
    {
        string FindEmailById(int id);
        string FindEmailBySTK(string stk);
        int CheckEmailExits(string email);
        bool SendMail(EmailDTO emailDTO);
        bool SendMailBySTK(string stk, int transactionId);
        bool SendMailByEmail(string email);
    }
}
