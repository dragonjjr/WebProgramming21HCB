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
        int CheckEmailExits(string email);
        bool SendMail(EmailDTO emailDTO);
        bool SendMailById(int id, int transactionId);
        bool SendMailByEmail(string email);
    }
}
