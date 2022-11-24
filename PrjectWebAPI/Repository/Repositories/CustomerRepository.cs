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

        /// <summary>
        /// Tìm người thụ hưởng/ người nhận theo Id
        /// </summary>
        /// <returns></returns>
        public Recipient FindRecipientById(int id)
        {
            return dbContext.Recipients.Find(id);
        }

        /// <summary>
        /// Lấy thông tin khách hàng theo id
        /// </summary>
        /// <returns></returns>
        public UserManage FindUserById(int id)
        {
            return dbContext.UserManages.Find(id);
        }


        /// <summary>
        /// Tìm người nhận/ người thụ hưởng theo stk và id người gửi
        /// </summary>
        /// <returns></returns>
        public bool FindRecipientByStkAndUserId(RecipientInput recipientInput)
        {
            try
            {
                var existRecipient = dbContext.Recipients.Where(x => x.Stk == recipientInput.STK && x.UserId == recipientInput.UserID).FirstOrDefault();
                if (existRecipient != null)
                {
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

        /// <summary>
        /// Thêm mới người thụ hưởng/ người nhận
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Xóa người thụ hưởng/ người nhận
        /// </summary>
        /// <returns></returns>
        public bool DeleteRecipient(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

        /// <summary>
        /// Lấy thông tin số dư của khách hàng
        /// </summary>
        /// <returns></returns>
        public UserBalance GetUserBalance(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }

        /// <summary>
        /// Lấy danh sách người thụ hưởng/ người nhận của khách hàng
        /// </summary>
        /// <returns></returns>
        public List<RecipientOutput> GetListRecipientByUserId(int id)
        {
            try
            {
                var existUser = FindUserById(id);
                if (existUser != null)
                {
                    return dbContext.Recipients.Where(x => x.UserId == id).Select(x => new RecipientOutput() { Id = x.Id, Name = x.Name, STK = x.Stk }).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }

        /// <summary>
        /// Cập nhật người thụ hưởng/ người nhận
        /// </summary>
        /// <returns></returns>
        public bool UpdateRecipient(int id ,RecipientEdit recipientEdit)
        {
            try
            {
                var existRecipient = FindRecipientById(id);
                if (existRecipient != null)
                {
                    existRecipient.Stk = recipientEdit.STK;
                    existRecipient.Name = recipientEdit.Name;
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
    }
}
