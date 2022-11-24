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
        bool Login(AccountInput accountInput);

        bool ChangePassword(ChangePasswordInput changePasswordInput);

        AccountTokenInfor GetUserIdAndToken(string username);
    }
}
