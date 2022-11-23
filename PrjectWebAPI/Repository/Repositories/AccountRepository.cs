using Common;
using Repository.DBContext;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly _6IVYVvfe0wContext dbContext;
        public AccountRepository(_6IVYVvfe0wContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        public bool Login(AccountInput accountInput)
        {
            var result = dbContext.Accounts.Where(x => x.Username == accountInput.Username && x.Password == accountInput.Password).FirstOrDefault();
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AuthOTP()
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(ChangePasswordInput changePasswordInput)
        {
            if (changePasswordInput.NewPassword == changePasswordInput.ConfirmNewPassword)
            {
                string passwordInputHash = BCrypt.Net.BCrypt.HashPassword(changePasswordInput.CurrentPassword);
                var oldPassword = dbContext.Accounts.Where(x => x.Id == changePasswordInput.Id).Select(x=>x.Password).SingleOrDefault();
                bool verified = BCrypt.Net.BCrypt.Verify(oldPassword, passwordInputHash);
                if (verified)
                {
                    oldPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordInput.NewPassword);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public AccountTokenInfor GetUserIdAndToken(string username)
        {
            return dbContext.Accounts.Where(x => x.Username == username).Select(x => new AccountTokenInfor() {Id =  x.Id, RefreshToken = x.RefreshToken }).FirstOrDefault();
        }

        public int GetUserIdByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
