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
            try
            {
                if (!string.IsNullOrEmpty(VM.Username) && !string.IsNullOrEmpty(VM.Password))
                {
                    string pwd = VM.Password;
                    using (var sha256 = SHA256.Create())
                    {
                        var user = dbContext.Accounts.SingleOrDefault(x => x.Username == VM.Username);

                        if (user != null &&  BCrypt.Net.BCrypt.Verify(VM.Password, user.Password))
                        {
                            var authClaims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name , user.Username),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim("userId" , user.Id.ToString()),
                            };
                            var token = Helpers.CreateToken(authClaims);
                            var refreshToken = Helpers.GenerateRefreshToken();
                            user.RefreshToken = refreshToken;
                            user.ExpiredDate = DateTime.Now.AddDays(Helpers.JWT_Time);
                            rs.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
                            rs.RefreshToken = refreshToken;
                            rs.Status = 200;
                            rs.LoggedIn = true;
                            rs.isStaff = dbContext.UserManages.Where(x => x.Id == user.Id).Select(x => x.IsStaff).FirstOrDefault();
                            dbContext.Accounts.Update(user);
                            dbContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
            return rs;
        }
 

        public bool ChangePassword(int Id, ChangePasswordInput changePasswordInput)
        {
            try
            {
                if (changePasswordInput.NewPassword == changePasswordInput.ConfirmNewPassword)
                {
                    string currentPassword = changePasswordInput.CurrentPassword;
                    var account = dbContext.Accounts.Where(x => x.Id == Id).SingleOrDefault();
                    string oldPassword = account.Password;
                    bool verified = BCrypt.Net.BCrypt.Verify(currentPassword,oldPassword);
                    if (verified)
                    {
                        account.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordInput.NewPassword);
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
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        public AccountTokenInfor GetUserIdAndToken(string username)
        {
            return dbContext.Accounts.Where(x => x.Username == username).Select(x => new AccountTokenInfor() {Id =  x.Id, RefreshToken = x.RefreshToken }).FirstOrDefault();
        }

        public int GetAccountIdByEmail(string email)
        {
            return dbContext.UserManages.Where(x => x.Email == email).Select(x => x.Id).FirstOrDefault();
        }

        public ResponeseMessage ResetPassword(int Id,ResetPasswordInput resetPasswordInput)
        {
            ResponeseMessage rs = new ResponeseMessage();
            // Select OTP Code from db
            var OTPCode = dbContext.OtpTables.Where(x => x.UserId == Id).OrderByDescending(T => T.CreateDate).FirstOrDefault().Otp;
            //Compare OTP account input vs OTP from db
            try
            {
                if (OTPCode != null && OTPCode == resetPasswordInput.OTPCode)
                {
                    if (resetPasswordInput.NewPassword == resetPasswordInput.ConfirmNewPassword)
                    {
                        var account = dbContext.Accounts.Where(x => x.Id == Id).FirstOrDefault();
                        account.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordInput.NewPassword);
                        dbContext.SaveChanges();
                        rs.Status = 200;
                        rs.Message = "Reset password successfully!";
                    }
                    else
                    {
                        rs.Status = 0;
                        rs.Message = "Confirm password incorrect!";
                    }
                }
                else
                {
                    rs.Status = 0;
                    rs.Message = "OTP code invalid!";
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
            return rs;
        }
    }
}
