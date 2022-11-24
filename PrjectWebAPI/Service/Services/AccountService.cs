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

        public bool ChangePassword(int Id, ChangePasswordInput changePasswordInput)
        {
            return _IaccountRepository.ChangePassword(Id, changePasswordInput);
        }

        public int GetAccountIdByEmail(string email)
        {
            return _IaccountRepository.GetAccountIdByEmail(email);
        }

        public AccountTokenInfor GetUserIdAndToken(string username)
        {
            return _IaccountRepository.GetUserIdAndToken(username);
        }

        public LoginOutput Login(AccountInput accountInput)
        {
            return _IaccountRepository.Login(accountInput);
        }

        public ResponeseMessage ResetPassword(int Id, ResetPasswordInput resetPasswordInput)
        {
            return _IaccountRepository.ResetPassword(Id, resetPasswordInput);
        }
    }
}
