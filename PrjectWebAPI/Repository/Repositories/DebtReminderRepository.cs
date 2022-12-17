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
        private IInternalRepository IinternalRepository;
        public DebtReminderRepository(_6IVYVvfe0wContext dbContext, IInternalRepository IinternalRepository)
        {
            this.dbContext = dbContext;
            this.IinternalRepository = IinternalRepository;
        }

        /// <summary>
        /// Create debt remind
        /// </summary>
        /// <returns></returns>
        public bool CreateDebtRemind(DebtRemindInput debtRemindInfo)
        {
            try
            {

                var receiveAcc = dbContext.UserManages.Where(user => user.Stk == debtRemindInfo.STKReceive).FirstOrDefault();
                var sendAcc = dbContext.UserManages.Where(user => user.Stk == debtRemindInfo.STKSend).FirstOrDefault();

                if (receiveAcc != null && sendAcc != null)
                {
                    // Add debt remind
                    DebtReminder debtRemind = new DebtReminder();
                    debtRemind.Stksend = debtRemindInfo.STKSend;
                    debtRemind.Stkreceive = debtRemindInfo.STKReceive;
                    debtRemind.SoTien = debtRemindInfo.Money;
                    debtRemind.NoiDung = debtRemindInfo.Content;
                    debtRemind.Status = 0; // new debt remind
                    debtRemind.CreatedDate = DateTime.Now;
                    debtRemind.UpdatedDate = DateTime.Now;

                    dbContext.DebtReminders.Add(debtRemind);

                    if (dbContext.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
                return false;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CancelDebtRemind(int debtRemindID)
        {
            using var _trans = dbContext.Database.BeginTransaction();

            try
            {
                _trans.CreateSavepoint("BeforeCancelDebtRemind");

                var debtRemindInfo = dbContext.DebtReminders.Where(x => x.IsDeleted == false && x.Id == debtRemindID && x.Status == 0).FirstOrDefault();

                if (debtRemindInfo != null)
                {
                    debtRemindInfo.IsDeleted = true;
                    debtRemindInfo.Status = 2;
                    debtRemindInfo.UpdatedDate = DateTime.Now;


                    // Add notification
                    Notification notice = new Notification();
                    notice.Stksend = debtRemindInfo.Stksend;
                    notice.Stkreceive = debtRemindInfo.Stkreceive;
                    notice.CreatedDate = DateTime.Now;
                    notice.UpdatedDate = DateTime.Now;
                    notice.Content = "Cancel debt remind: " + debtRemindInfo.SoTien + "$ from account " + debtRemindInfo.Stksend;

                    dbContext.Notifications.Add(notice);
                    dbContext.SaveChanges();
                    _trans.Commit();
                    return true;

                }

                return false;

            }
            catch (Exception e)
            {
                _trans.RollbackToSavepoint("BeforeCancelDebtRemind");
                return false;
            }
        }

        public bool payDebtRemind(int debtRemindID)
        {

            try
            {

                var debtRemindInfo = dbContext.DebtReminders.Where(x => x.IsDeleted == false && x.Id == debtRemindID && x.Status == 0).FirstOrDefault();

                if (debtRemindInfo != null)
                {
                    var sendUser = dbContext.UserManages.Where(x => x.IsDeleted == false && x.Stk == debtRemindInfo.Stksend).FirstOrDefault();

                    // pay
                    InternalTransfer infoTrans = new InternalTransfer
                    {
                        Send_UserID = sendUser.Id,
                        Send_STK = debtRemindInfo.Stksend,
                        Receive_STK = debtRemindInfo.Stkreceive,
                        Send_Money = debtRemindInfo.SoTien,
                        Content = "Payment for debt remind",
                        PaymentFeeTypeID = 2, // Transferr pays fee
                        TransactionTypeID = 1, // internal transfer
                        isDebtRemind = true,
                    };

                    bool isSuccess = IinternalRepository.InternalTransfer(infoTrans);

                    if (isSuccess)
                    {
                        debtRemindInfo.Status = 1; // paid
                        debtRemindInfo.UpdatedDate = DateTime.Now;

                        // Add notification
                        Notification notice = new Notification();
                        notice.Stksend = debtRemindInfo.Stksend;
                        notice.Stkreceive = debtRemindInfo.Stkreceive;
                        notice.CreatedDate = DateTime.Now;
                        notice.UpdatedDate = DateTime.Now;
                        notice.Content = "Payment debt remind: paid " + debtRemindInfo.SoTien + "$ from account " + debtRemindInfo.Stksend;

                        dbContext.Notifications.Add(notice);

                        if(dbContext.SaveChanges()>0)
                        {
                            return true;
                        }
                        return false;
                        
                    }
                }


                return false;

            }
            catch (Exception e)
            {
                return false;
            }
        }




        public List<DebtRemindInfo> viewInfoDebtReminds(string STK, bool isSelf, int? status) // Status = 0: not paid, status = 1: paid, status = null: get all
        {
            try
            {
                // Check param
                if (status != null && (status < 0 || status > 1))
                    return null;

                var acc = dbContext.UserManages.Where(x => x.Stk == STK).FirstOrDefault();

                if (acc != null)
                {
                    // If debt remind is created by self
                    if (isSelf)
                    {

                        var records = dbContext.DebtReminders.Where(x => x.Stksend == STK && x.IsDeleted == false && (status == 0 ? x.Status == 0 : (status == 1 ? x.Status == 1 : true))).Select(debtRemind => new DebtRemindInfo
                        {
                            Id = debtRemind.Id,
                            STK = debtRemind.Stkreceive,
                            Content = debtRemind.NoiDung,
                            Money = debtRemind.SoTien
                        }).ToList();

                        return records;
                    }
                    else
                    {
                        var records = dbContext.DebtReminders.Where(x => x.Stkreceive == STK && x.IsDeleted == false && (status == 0 ? x.Status == 0 : (status == 1 ? x.Status == 1 : true))).Select(debtRemind => new DebtRemindInfo
                        {
                            Id = debtRemind.Id,
                            STK = debtRemind.Stksend,
                            Content = debtRemind.NoiDung,
                            Money = debtRemind.SoTien
                        }).ToList();

                        return records;
                    }

                }

                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
