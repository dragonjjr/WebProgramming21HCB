using Common;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _IaccountRepository;

        public AccountService(IAccountRepository IaccountRepository)
        {
            _IaccountRepository = IaccountRepository;
        }

        public bool ChangePassword(ChangePasswordInput changePasswordInput)
        {
            return _IaccountRepository.ChangePassword(changePasswordInput);
        }

        public AccountTokenInfor GetUserIdAndToken(string username)
        {
            return _IaccountRepository.GetUserIdAndToken(username);
        }

        public LoginOutput Login(AccountInput accountInput)
        {
            return _IaccountRepository.Login(accountInput);
        }
    }
}
