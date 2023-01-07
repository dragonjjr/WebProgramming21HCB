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
            return dbContext.UserManages.Where(u=>u.Id==id && u.IsDeleted==false).Single();
        }


        /// <summary>
        /// Tìm người nhận/ người thụ hưởng theo stk và id người gửi
        /// </summary>
        /// <returns></returns>
        public bool FindRecipientByStkAndUserId(RecipientInput recipientInput)
        {
            try
            {
                var existRecipient = dbContext.Recipients.Where(x => x.Stk == recipientInput.STK && x.UserId == recipientInput.UserID && x.IsDeleted != true).FirstOrDefault();
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

                
                if (String.IsNullOrEmpty(recipientInput.Name))
                {
                    var userExist = dbContext.UserManages.Where(x => x.Stk == recipientInput.STK).Select(x=>x.Name).FirstOrDefault();
                    recipientInput.Name = userExist;
                }
                if (existRecipient == false)
                {

                    Recipient recipient = new Recipient();
                    recipient.Stk = recipientInput.STK;
                    recipient.Name = recipientInput.Name;
                    recipient.UserId = recipientInput.UserID;
                    recipient.BankId = recipientInput.BankID;
                    recipient.CreatedDate = DateTime.Now;
                    recipient.UpdatedDate = DateTime.Now;

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
                    recipient.IsDeleted = true;
                    recipient.UpdatedDate = DateTime.Now;

                    dbContext.Recipients.Update(recipient);
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
                    return dbContext.Recipients.Where(x => x.UserId == id && x.IsDeleted != true).Select(x => new{ Id = x.Id, Name = x.Name, STK = x.Stk,  BankID = x.BankId }).ToList()
                        .Join(dbContext.BankReferences,d1=> d1.BankID,d2 => d2.Id, (d1,d2) => new RecipientOutput() { Id = d1.Id, Name = d1.Name, STK = d1.STK, Bank = new BankReferenceVM() { Id = d2.Id, Name = d2.Name} }).ToList();
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
                    existRecipient.BankId = recipientEdit.BankID;
                    existRecipient.UpdatedDate = DateTime.Now;
                    
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
        /// Lấy danh sách ngân hàng liên kết
        /// </summary>
        /// <returns></returns>
        public List<BankReferenceVM> GetBankReferences()
        {
            try
            {
                return dbContext.BankReferences.Select(x=> new BankReferenceVM() { Id = x.Id, Name = x.Name}).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
