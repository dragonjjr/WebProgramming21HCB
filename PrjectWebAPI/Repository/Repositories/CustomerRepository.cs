using Common;
using Repository.DBContext;
using Repository.Interfaces;
using System;
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
        public Recipient FindById(int id)
        {
            return dbContext.Recipients.Find(id);
        }

        public bool FindByStkAndUserId(RecipientInput recipientInput)
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
                bool existRecipient = FindByStkAndUserId(recipientInput);
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
            var recipient = FindById(id);
            if(recipient!= null)
            {
                dbContext.Recipients.Remove(recipient);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
