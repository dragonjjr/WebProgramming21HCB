using Common;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IDebtReminderService
    {
        bool CreateDebtRemind(DebtRemindInput debtRemindInfo);
        bool CancelDebtRemind(int debtRemindID);
        bool payDebtRemind(int debtRemindID);
        List<DebtRemindInfo> viewInfoDebtReminds(string STK, bool isSelf, int? status);
    }
}
