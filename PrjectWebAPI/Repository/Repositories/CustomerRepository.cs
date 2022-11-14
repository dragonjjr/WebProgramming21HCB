using Common;
using Repository.DBContext;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly _6IVYVvfe0wContext dbContext;
        public CustomerRepository(_6IVYVvfe0wContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public bool AddRecipient(RecipientInput recipientInput)
        {
            try
            {
                Recipient recipient = new Recipient();
                recipient.Stk = recipientInput.STK;
                recipient.Name = recipientInput.Name;
                recipient.UserId = recipientInput.UserID;


                dbContext.Recipients.Add(recipient);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
    }
}
