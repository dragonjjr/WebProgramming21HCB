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
        private DebtReminderService _IDebtReminderService;

        public DebtReminderService(DebtReminderService _IAdministratorRepository)
        {
            this._IDebtReminderService = _IAdministratorRepository;
        }

       
    }
}
