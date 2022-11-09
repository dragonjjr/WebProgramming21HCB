using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        bool RegisterAccount(AccountViewModel accountViewModel);
        bool Recharge(RechargeInput rechargeInput);
        AccountViewModel GetAccountInfor(AccountInforInput infor);
    }
}
