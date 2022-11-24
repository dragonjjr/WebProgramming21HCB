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
<<<<<<< HEAD
        bool Login(AccountInput accountInput);
=======
        LoginOutput Login(AccountInput accountInput);
>>>>>>> ce5eef002baec05eb5b0e8632203b91ff43e4956

        bool ChangePassword(ChangePasswordInput changePasswordInput);

        AccountTokenInfor GetUserIdAndToken(string username);
    }
}
