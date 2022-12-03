﻿using Common;
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
                    DebtReminder debtRemind = new DebtReminder();
                    debtRemind.Stksend = debtRemindInfo.STKSend;
                    debtRemind.Stkreceive = debtRemindInfo.STKReceive;
                    debtRemind.SoTien = debtRemindInfo.Money;
                    debtRemind.NoiDung = debtRemindInfo.Content;
                    debtRemind.Status = 0; // new debt remind

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
            try
            {
                var row = dbContext.DebtReminders.Find(debtRemindID);

                if(row!=null)
                {
                    row.IsDeleted = true;

                    if(dbContext.SaveChanges()>0)
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

        public bool payDebtRemind(int debtRemindID)
        {
            try
            {
                var row = dbContext.DebtReminders.Where(x=>x.IsDeleted==false && x.Id==debtRemindID).Single();

                if (row != null)
                {
                    row.Status = 1;

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

        public List<DebtRemindInfo> viewInfoDebtReminds(string STK, bool isSelf, int? status) // Status = 0: not paid, status = 1: paid, status = null: get all
        {
            try
            {
                // Check param
                if (status!=null && (status < 0 || status > 1))
                    return null;

                var acc = dbContext.UserManages.Where(x => x.Stk == STK).FirstOrDefault();
               
                if(acc!=null)
                {
                    // If debt remind is created by self
                    if (isSelf)
                    {

                        var records = dbContext.DebtReminders.Where(x => x.Stksend == STK && x.IsDeleted==false && (status == 0 ? x.Status == 0 : (status == 1 ? x.Status == 1 : true))).Select(debtRemind => new DebtRemindInfo
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
                        var records = dbContext.DebtReminders.Where(x => x.Stkreceive == STK && x.IsDeleted == false  && (status == 0 ? x.Status == 0 : (status == 1 ? x.Status == 1 : true))).Select(debtRemind => new DebtRemindInfo
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