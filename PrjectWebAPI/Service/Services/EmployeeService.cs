using Common;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EmployeeService:IEmployeeService
    {
        private IEmployeeRepository _IemployeeRepository;
        public EmployeeService(IEmployeeRepository IemployeeRepository)
        {
            _IemployeeRepository = IemployeeRepository;
        }
        public string RegisterAccount(AccountViewModel accountViewModel)
        {
            return _IemployeeRepository.RegisterAccount(accountViewModel);
        }

        public decimal Recharge(RechargeInput rechargeInput)
        {
            return _IemployeeRepository.Recharge(rechargeInput);
        }

        public AccountViewModel GetAccountInfor(AccountInforInput infor)
        {
            return _IemployeeRepository.GetAccountInfor(infor);
        }
    }
}
