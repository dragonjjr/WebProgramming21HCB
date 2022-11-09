using Common;
using Microsoft.EntityFrameworkCore;
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

        public bool RegisterAccount(AccountViewModel accountViewModel)
        {
            try
            {
                UserManage userManage = new UserManage();
                userManage.Name = accountViewModel.Infor.Name;
                userManage.Cmnd = accountViewModel.Infor.Cmnd;
                userManage.Address = accountViewModel.Infor.Address;
                userManage.BankKind = accountViewModel.Infor.BankKind;
                userManage.Stk = accountViewModel.Infor.Stk;
                userManage.Phone = accountViewModel.Infor.Phone;
                userManage.Email = accountViewModel.Infor.Email;
                userManage.CreatedDate = DateTime.Now;

                dbContext.UserManages.Add(userManage);
                dbContext.SaveChanges();

                Account ac = new Account();
                ac.Username = accountViewModel.UserName;
                ac.Role = accountViewModel.RoleID.ToString();
                ac.Password = accountViewModel.Password;
                ac.Id = userManage.Id;

                dbContext.Accounts.Add(ac);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }

        }


        public bool Recharge(RechargeVM rechargeVM)
        {
            try
            {
                var account = dbContext.UserManages.Where(x => x.BankKind == rechargeVM.BankID.ToString() && x.Stk == rechargeVM.STK_Receive).FirstOrDefault();
                if (account != null)
                {
                    TransactionBanking tb = new TransactionBanking
                    {
                        Stksend = rechargeVM.STK_Send,
                        Stkreceive = rechargeVM.STK_Receive,
                        BankReferenceId = rechargeVM.BankID,
                        TransactionTypeId = rechargeVM.TransactionTypeId,
                        PaymentFeeTypeId = rechargeVM.PaymentTypeID,

                    };
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
