using Common;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class DebtReminderService : IDebtReminderService
    {
        private IDebtReminderRepository _IDebtReminderRepository;

        public DebtReminderService(IDebtReminderRepository _IDebtReminderRepository)
        {
            this._IDebtReminderRepository = _IDebtReminderRepository;
        }

        public bool CreateDebtRemind(DebtRemindInput debtRemindInfo)
        {
            return this._IDebtReminderRepository.CreateDebtRemind(debtRemindInfo);
        }

        public bool CancelDebtRemind(int debtRemindID)
        {
            return this._IDebtReminderRepository.CancelDebtRemind(debtRemindID);
        }

        public bool payDebtRemind(int debtRemindID)
        {
            return this._IDebtReminderRepository.payDebtRemind(debtRemindID);
        }


        public List<DebtRemindInfo> viewInfoDebtReminds(string STK, bool isSelf, int? status)
        {
            return this._IDebtReminderRepository.viewInfoDebtReminds(STK, isSelf, status);
        }
    }
}
