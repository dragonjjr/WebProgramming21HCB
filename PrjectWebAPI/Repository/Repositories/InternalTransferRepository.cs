using Common;
using Org.BouncyCastle.Bcpg;
using Repository.DBContext;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Repository.Repositories
{
    public class InternalTransferRepository:IInternalRepository
    {
        private readonly _6IVYVvfe0wContext dbContext;
        public InternalTransferRepository(_6IVYVvfe0wContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        /// <summary>
        /// Lấy danh sách hình thức thanh toán
        /// </summary>
        /// <returns></returns>
        public List<PaymentFeeTypeVM> GetPaymentFeeType()
        {
            var rs = new List<PaymentFeeTypeVM>();
            var model = dbContext.PaymentFeeTypes.Where(x => x.IsDeleted == false).ToList();
            if(model.Count() > 0)
            {
                for(int i =0; i< model.Count(); i++)
                {
                    var row = new PaymentFeeTypeVM
                    {
                        ID = model[i].Id,
                        Name = model[i].Name
                    };
                    rs.Add(row);
                }    
            } 
            return rs;
        }


        /// <summary>
        /// Kiểm tra OTP giao dịch chuyển tiền
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CheckOTPTransaction(CheckOTPTransaction model)
        {
            try
            {
                if(model.TransactionID > 0 && model.OTP != string.Empty)
                {
                    var row = dbContext.OtpTables.Where(x => x.TransactionId == model.TransactionID && x.Otp == model.OTP && x.CreateDate < DateTime.Now && x.ExpiredDate <= DateTime.Now).FirstOrDefault();
                    if (row != null)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Xem thông tin người nhận bằng STK
        /// </summary>
        /// <param name="STK"></param>
        /// <returns></returns>
        public RecipientOutput ViewRecipientBySTK(string STK)
        {
            try
            {
                if (STK != string.Empty)
                {
                    var row = dbContext.Recipients.Where(x => x.Stk == STK).Select(x=> new RecipientOutput { Id = x.Id, Name = x.Name, STK = x.Stk}).FirstOrDefault();
                    if (row != null)
                    {
                        return row;
                    }
                    return null;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// Lấy danh sách tài khoản thanh toán
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public UserViewModel GetListAccount(int UserID)
        {
            try
            {
                if (UserID > 0)
                {
                    var row = dbContext.UserManages.Where(x => x.Id == UserID).Select(x => new UserViewModel {
                        Name = x.Name,
                        Cmnd = x.Cmnd,
                        Address = x.Address,
                        Stk = x.Stk,
                        SoDu = x.SoDu,
                        BankKind = x.BankKind,
                        Email = x.Email,
                        Phone = x.Phone,
                        IsStaff = x.IsStaff
                    }).FirstOrDefault();
                    if (row != null)
                    {
                        return row;
                    }
                    return null;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Chuyển khoản nội bộ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InternalTransfer(InternalTransfer model)
        {
            using var _trans = dbContext.Database.BeginTransaction();
            try
            {
                _trans.CreateSavepoint("BeforeTransfer");
                if(model.Send_UserID > 0 && model.Send_STK != string.Empty && model.Send_Money > 0 && model.Receive_STK != String.Empty)
                {
                    var send_acc = dbContext.UserManages.Where(x => x.Id == model.Send_UserID && x.Stk == model.Send_STK && x.SoDu > model.Send_Money).FirstOrDefault();
                    var receive_acc = dbContext.UserManages.Where(x => x.Stk == model.Receive_STK).FirstOrDefault();
                    if(send_acc != null && receive_acc != null)
                    {
                        var transaction = new TransactionBanking
                        {
                            Stksend = model.Send_STK,
                            Stkreceive = model.Receive_STK,
                            Content = model.Content,
                            Money = model.Send_Money,
                            TransactionTypeId = model.TransactionTypeID,
                            PaymentFeeTypeId = model.PaymentFeeTypeID,
                            BankReferenceId = model.BankReferenceId,
                            CreatedDate = DateTime.Now
                        };
                        dbContext.TransactionBankings.Add(transaction);
                        send_acc.SoDu = send_acc.SoDu - model.Send_Money;
                        receive_acc.SoDu = receive_acc.SoDu + model.Send_Money;
                        dbContext.UserManages.Update(send_acc);
                        dbContext.UserManages.Update(receive_acc);
                        dbContext.SaveChanges();
                        _trans.Commit();
                        return true;
                    } 
                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                _trans.RollbackToSavepoint("BeforeTransfer");
                return false;
            }
        }


        /// <summary>
        /// Chuyển khoản liên ngân hàng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ExternalTransfer(ExternalTransfer model)
        {
            using var _trans = dbContext.Database.BeginTransaction();
            try
            {
                _trans.CreateSavepoint("BeforeTransfer");
                if (model.Send_UserID > 0 && model.Send_STK != string.Empty && model.Send_Money > 0 && model.Receive_BankID > 0 && model.Receive_STK != String.Empty)
                {
                    var send_acc = dbContext.UserManages.Where(x => x.Id == model.Send_UserID && x.Stk == model.Send_STK && x.SoDu > model.Send_Money).FirstOrDefault();
                    
                    if (send_acc != null)
                    {
                        var transaction = new TransactionBanking
                        {
                            Stksend = model.Send_STK,
                            Stkreceive = model.Receive_STK,
                            BankReferenceId = model.Receive_BankID,
                            Content = model.Content,
                            Money = model.Send_Money,
                            TransactionTypeId = model.TransactionTypeID,
                            PaymentFeeTypeId = model.PaymentFeeTypeID,
                            CreatedDate = DateTime.Now
                        };
                        dbContext.TransactionBankings.Add(transaction);
                        send_acc.SoDu = send_acc.SoDu - model.Send_Money;
                        dbContext.UserManages.Update(send_acc);
                        dbContext.SaveChanges();
                        _trans.Commit();
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                _trans.RollbackToSavepoint("BeforeTransfer");
                return false;
            }
        }
    }
}
