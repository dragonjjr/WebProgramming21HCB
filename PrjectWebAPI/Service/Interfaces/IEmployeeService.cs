using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEmployeeService
    {
        bool RegisterAccount(AccountViewModel accountViewModel);
        AccountViewModel GetAccountInfor(AccountInforInput infor);
        bool Recharge(RechargeInput rechargeInput);
    }
}
