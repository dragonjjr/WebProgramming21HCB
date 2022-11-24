using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAccountService
    {
        LoginOutput Login(AccountInput accountInput);

        bool ChangePassword(int Id, ChangePasswordInput changePasswordInput);

        AccountTokenInfor GetUserIdAndToken(string username);

        int GetAccountIdByEmail(string email);

        ResponeseMessage ResetPassword(int Id, ResetPasswordInput resetPasswordInput);
    }
}
