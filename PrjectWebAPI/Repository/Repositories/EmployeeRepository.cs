using Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
        private readonly d3bphnumi39q70Context dbContext;
        public EmployeeRepository(d3bphnumi39q70Context _dbContext)
        {
            this.dbContext = _dbContext;
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
    }
}
