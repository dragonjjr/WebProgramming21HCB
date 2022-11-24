using Common;
using Repository.DBContext;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
        public LoginOutput Login(AccountInput VM)
        {
            LoginOutput rs = new LoginOutput();
            if (!string.IsNullOrEmpty(VM.Username) && !string.IsNullOrEmpty(VM.Password))
            {
                string pwd = VM.Password;
                using (var sha256 = SHA256.Create())
                {
                    //byte[] PasswordAsBytes = Encoding.UTF8.GetBytes(VM.Password);
                    //string pwd = Convert.ToBase64String(PasswordAsBytes);
                    Account user = dbContext.Accounts.Where(x => x.Username == VM.Username && x.Password == pwd).FirstOrDefault();

                    if (user != null)
                    {
                        var authClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Username),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };
                        var token = Helpers.CreateToken(authClaims);
                        var refreshToken = Helpers.GenerateRefreshToken();
                        user.RefreshToken = refreshToken;
                        user.ExpiredDate = DateTime.Now.AddDays(Helpers.JWT_Time);
                        rs.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
                        rs.RefreshToken = refreshToken;
                        rs.Status = 200;
                        rs.LoggedIn = true;
                        dbContext.Accounts.Update(user);
                        dbContext.SaveChanges();
                    }
                }
            }
            return rs;
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
