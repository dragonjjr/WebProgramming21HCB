using Common;
using Repository.DBContext;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IDebtReminderRepository
    {
        bool CreateDebtRemind(DebtRemindInput debtRemindInfo);
        bool CancelDebtRemind(int debtRemindID);
        bool payDebtRemind(int debtRemindID);
        List<DebtRemindInfo> viewInfoDebtReminds(string STK, bool isSelf, int? status);
    }
}
