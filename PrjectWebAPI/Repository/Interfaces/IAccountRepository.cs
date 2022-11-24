using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAccountRepository
    {
        LoginOutput Login(AccountInput accountInput);

        bool AuthOTP();

        bool ChangePassword(ChangePasswordInput  changePasswordInput);

        AccountTokenInfor GetUserIdAndToken(string username);

        int GetUserIdByEmail(string email);
    }
}
