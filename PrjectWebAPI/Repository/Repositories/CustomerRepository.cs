using Common;
using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Repository.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly _6IVYVvfe0wContext dbContext;
        public CustomerRepository(_6IVYVvfe0wContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        public Recipient FindRecipientById(int id)
        {
            return dbContext.Recipients.Find(id);
        }
        public UserManage FindUserById(int id)
        {
            return dbContext.UserManages.Find(id);
        }

        public bool FindRecipientByStkAndUserId(RecipientInput recipientInput)
        {
            var existRecipient = dbContext.Recipients.Where(x => x.Stk == recipientInput.STK && x.UserId == recipientInput.UserID).FirstOrDefault();
            if (existRecipient != null)
            {
                return true;
            }
            return false;
        }

        public bool AddRecipient(RecipientInput recipientInput)
        {
            try
            {
                bool existRecipient = FindRecipientByStkAndUserId(recipientInput);
                if (existRecipient == false)
                {

                    Recipient recipient = new Recipient();
                    recipient.Stk = recipientInput.STK;
                    recipient.Name = recipientInput.Name;
                    recipient.UserId = recipientInput.UserID;


                    dbContext.Recipients.Add(recipient);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        public bool DeleteRecipient(int id)
        {
            var recipient = FindRecipientById(id);
            if (recipient != null)
            {
                dbContext.Recipients.Remove(recipient);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public UserBalance GetUserBalance(int id)
        {
            var existUser = FindUserById(id);
            if (existUser != null)
            {
                UserBalance userBalance = new UserBalance();
                userBalance.SoDu = existUser.SoDu;
                userBalance.STK = existUser.Stk;
                return userBalance;
            }
            return null;
        }

        public List<RecipientOutput> GetListRecipientByUserId(int id)
        {
            var existUser = FindUserById(id);
            if (existUser != null)
            {
                return dbContext.Recipients.Where(x => x.UserId == id).Select(x => new RecipientOutput() { Id = x.Id, Name = x.Name, STK = x.Stk }).ToList();
            }
            return null;
        }

        public bool UpdateRecipient(int id ,RecipientEdit recipientEdit)
        {
            var existRecipient = FindRecipientById(id);
            if(existRecipient != null)
            {
                existRecipient.Stk = recipientEdit.STK;
                existRecipient.Name = recipientEdit.Name;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
