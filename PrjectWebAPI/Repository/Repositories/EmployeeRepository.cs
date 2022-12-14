using Common;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using Repository.DBContext;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly _6IVYVvfe0wContext dbContext;
        public EmployeeRepository(_6IVYVvfe0wContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        /// <summary>
        /// Xem thông tin tài khoản
        /// </summary>
        /// <param name="infor"></param>
        /// <returns></returns>
        public AccountViewModel GetAccountInfor(AccountInforInput infor)
        {
            AccountViewModel rs = new AccountViewModel();
            var ac = dbContext.Accounts.FromSqlRaw("Select * from Account").FirstOrDefault();
            if (ac != null)
            {
                rs.UserName = ac.Username;
                rs.RoleID = int.Parse(ac.Role);
            }
            return rs;
        }

        /// <summary>
        /// Đăng ký tài khoản
        /// </summary>
        /// <param name="accountViewModel"></param>
        /// <returns></returns>
        public string RegisterAccount(AccountViewModel accountViewModel)
        {
            try
            {
                var stk = dbContext.UserManages.Select(x => x.Stk).Max();
                var stk_new = int.Parse(stk.ToString()) < 1000000 ? 1000000 : int.Parse(stk.ToString()) + 1;
                UserManage userManage = new UserManage();
                userManage.Name = accountViewModel.Infor.Name;
                userManage.Cmnd = accountViewModel.Infor.Cmnd;
                userManage.Address = accountViewModel.Infor.Address==null?"": accountViewModel.Infor.Address;
                userManage.BankKind = "bankind";
                userManage.Stk = stk_new.ToString();
                userManage.Phone = accountViewModel.Infor.Phone;
                userManage.Email = accountViewModel.Infor.Email;
                userManage.CreatedDate = DateTime.Now;
                userManage.SoDu = 0;

                dbContext.UserManages.Add(userManage);
                dbContext.SaveChanges();

                Account ac = new Account();
                ac.Username = accountViewModel.UserName;
                ac.Role = accountViewModel.RoleID.ToString();
                ac.Password = BCrypt.Net.BCrypt.HashPassword(accountViewModel.Password);
                ac.Id = userManage.Id;

                dbContext.Accounts.Add(ac);

                if (dbContext.SaveChanges() > 0)
                {
                    return userManage.Stk.ToString();
                }
                else
                { 
                    return "0"; 
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }

        }

        /// <summary>
        /// Nạp tiền vào tài khoản
        /// </summary>
        /// <param name="rechargeInput"></param>
        /// <returns></returns>
        public decimal Recharge(RechargeInput rechargeInput)
        {
            try
            {
                var account = dbContext.UserManages.Where(x => x.Stk == rechargeInput.STK_Receive).FirstOrDefault();//x.BankKind == rechargeInput.BankID.ToString() &&
                if (account != null)
                {
                    TransactionBanking tb = new TransactionBanking
                    {
                        Stksend = rechargeInput.STK_Send,
                        Stkreceive = rechargeInput.STK_Receive,
                        BankReferenceId = rechargeInput.BankID,
                        TransactionTypeId = rechargeInput.TransactionTypeId,
                        PaymentFeeTypeId = rechargeInput.PaymentTypeID,
                        Money = rechargeInput.SoTien,
                        Content = rechargeInput.NoiDung,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    };
                    dbContext.TransactionBankings.Add(tb);
                    account.SoDu = account.SoDu + rechargeInput.SoTien;
                    dbContext.UserManages.Update(account);
                    dbContext.SaveChanges();
                    return (decimal)account.SoDu;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return 0;
            }
        }
    }
}
