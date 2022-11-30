using Common;
using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories
{
    public class DebtReminderRepository : IDebtReminderRepository
    {
        private readonly _6IVYVvfe0wContext dbContext;
        public DebtReminderRepository(_6IVYVvfe0wContext dbContext)
        {
            this.dbContext = dbContext;
        }

       
    }
}
