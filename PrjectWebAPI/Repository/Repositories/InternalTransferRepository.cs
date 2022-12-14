using Common;
using Repository.DBContext;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class InternalTransferRepository : IInternalRepository
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
            if (model.Count() > 0)
            {
                for (int i = 0; i < model.Count(); i++)
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
        public async Task<bool> CheckOTPTransaction(CheckOTPTransaction model, bool isInternalTranfer)
        {
            using var _trans = dbContext.Database.BeginTransaction();
            try
            {
                if (model.TransactionID > 0 && model.OTP != string.Empty)
                {
                    var transactioninput = dbContext.TransactionBankings.Where(x => x.Id == model.TransactionID).FirstOrDefault();
                    if (transactioninput != null)
                    {

                        // nếu là xác minh otp chuyển khoản nội bộ
                        if (isInternalTranfer)
                        {
                            var send_acc = dbContext.UserManages.Where(x => x.Stk == transactioninput.Stksend && x.SoDu > transactioninput.Money).FirstOrDefault();
                            var receive_acc = dbContext.UserManages.Where(x => x.Stk == transactioninput.Stkreceive).FirstOrDefault();

                            var row = dbContext.OtpTables.Where(x => x.TransactionId == model.TransactionID && x.Otp == model.OTP && DateTime.Now <= x.ExpiredDate).FirstOrDefault();

                            if (row != null)
                            {
                                transactioninput.IsDeleted = false;
                                send_acc.SoDu = send_acc.SoDu - transactioninput.Money;
                                receive_acc.SoDu = receive_acc.SoDu + transactioninput.Money;

                                dbContext.TransactionBankings.Update(transactioninput);
                                dbContext.UserManages.Update(send_acc);
                                dbContext.UserManages.Update(receive_acc);
                                dbContext.SaveChanges();
                                _trans.Commit();
                                return true;

                            }
                            return false;
                        }
                        // nếu là xác minh otp chuyển khoản liên ngân hàng
                        else
                        {
                            _trans.CreateSavepoint("BeforeTransfer");
                            var send_acc = dbContext.UserManages.Where(x => x.Stk == transactioninput.Stksend && x.SoDu > transactioninput.Money).FirstOrDefault();

                            var row = dbContext.OtpTables.Where(x => x.TransactionId == model.TransactionID && x.Otp == model.OTP && DateTime.Now <= x.ExpiredDate).FirstOrDefault();
                            if (row != null)
                            {
                                transactioninput.IsDeleted = false;
                                send_acc.SoDu = send_acc.SoDu - transactioninput.Money;
                                dbContext.UserManages.Update(send_acc);
                                dbContext.SaveChanges();

                                SendMoneyRequest data = new SendMoneyRequest
                                {
                                    sendPayAccount = transactioninput.Stksend,
                                    sendAccountName = "Nhóm 1",
                                    receiverPayAccount = transactioninput.Stkreceive,
                                    typeFee = transactioninput.PaymentFeeTypeId == 1 ? "sender" : "receiver",
                                    amountOwed = transactioninput.Money,
                                    bankReferenceId = "bank1",
                                    description = transactioninput.Content
                                };
                                HttpClient httpClient = new HttpClient();
                                HttpRequestMessage request = new HttpRequestMessage();
                                var httpContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                                DateTime datetime = DateTime.Now;
                                long time = ((DateTimeOffset)datetime).ToUnixTimeSeconds();
                                var hashtring = Helpers.SecretKey_Partner + $"/api/transaction/addmoney" + data.sendPayAccount + data.sendAccountName + data.receiverPayAccount + data.typeFee + data.amountOwed + data.bankReferenceId + time.ToString();
                                var token = Helpers.GetTokenOfPartner(hashtring);
                                request.RequestUri = new Uri(Helpers.url_Partner + $"api/transaction/addmoney");
                                request.Content = httpContent;
                                request.Method = HttpMethod.Post;
                                request.Headers.Add("signature", transactioninput.Rsa);
                                request.Headers.Add("token", token);
                                request.Headers.Add("time", time.ToString());

                                HttpResponseMessage response = await httpClient.SendAsync(request);
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    _trans.Commit();
                                    return true;
                                }

                                return false;

                            }
                            return false;

                        }

                    }
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
                    var row = dbContext.Recipients.Where(x => x.Stk == STK).Select(x => new RecipientOutput { Id = x.Id, Name = x.Name, STK = x.Stk }).FirstOrDefault();
                    var row1 = dbContext.UserManages.Where(x => x.Stk == STK).Select(x => new RecipientOutput { Id = x.Id, Name = x.Name, STK = x.Stk }).FirstOrDefault();
                    if (row != null)
                    {
                        return row;
                    }
                    else
                    {
                        if (row1 != null)
                        {
                            return row1;
                        }
                        else
                        {
                            return null;
                        }
                    }
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
                    var row = dbContext.UserManages.Where(x => x.Id == UserID).Select(x => new UserViewModel
                    {
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
        public int InternalTransfer(InternalTransfer model)
        {
            using var _trans = dbContext.Database.BeginTransaction();
            try
            {
                _trans.CreateSavepoint("BeforeTransfer");
                if (model.Send_UserID > 0 && model.Send_STK != string.Empty && model.Send_Money > 0 && model.Receive_STK != String.Empty)
                {
                    var send_acc = dbContext.UserManages.Where(x => x.Id == model.Send_UserID && x.Stk == model.Send_STK && x.SoDu > model.Send_Money).FirstOrDefault();
                    var receive_acc = dbContext.UserManages.Where(x => x.Stk == model.Receive_STK).FirstOrDefault();
                    if (send_acc != null && receive_acc != null)
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
                            IsDebtRemind = model.isDebtRemind,
                            CreatedDate = DateTime.Now,
                            IsDeleted = true,
                        };

                        dbContext.TransactionBankings.Add(transaction);
                        //send_acc.SoDu = send_acc.SoDu - model.Send_Money;
                        //receive_acc.SoDu = receive_acc.SoDu + model.Send_Money;
                        //dbContext.UserManages.Update(send_acc);
                        //dbContext.UserManages.Update(receive_acc);
                        dbContext.SaveChanges();
                        _trans.Commit();

                        return transaction.Id;
                    }
                    return 0;
                }
                return 0;
            }
            catch (Exception e)
            {
                _trans.RollbackToSavepoint("BeforeTransfer");
                return 0;
            }
        }


        /// <summary>
        /// Chuyển khoản liên ngân hàng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int ExternalTransfer(ExternalTransfer model)
        {
            using var _trans = dbContext.Database.BeginTransaction();
            try
            {
                _trans.CreateSavepoint("BeforeTransfer");
                if (model.Send_STK != string.Empty && model.Send_Money > 0 && model.Receive_BankID > 0 && model.Receive_STK != String.Empty)
                {
                    var send_acc = dbContext.UserManages.Where(x => x.Stk == model.Send_STK && x.SoDu > model.Send_Money).FirstOrDefault();

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
                            CreatedDate = DateTime.Now,
                            IsDebtRemind = false,
                            Rsa = model.RSA,
                            IsDeleted = true
                        };
                        dbContext.TransactionBankings.Add(transaction);
                        //send_acc.SoDu = send_acc.SoDu - model.Send_Money;
                       // dbContext.UserManages.Update(send_acc);
                        dbContext.SaveChanges();
                        _trans.Commit();


                        //SendMoneyRequest data = new SendMoneyRequest
                        //{
                        //    sendPayAccount = model.Send_STK,
                        //    sendAccountName = "Nhóm 1",
                        //    receiverPayAccount = model.Receive_STK,
                        //    typeFee = model.PaymentFeeTypeID == 1 ? "sender" : "receiver ",
                        //    amountOwed = model.Send_Money,
                        //    bankReferenceId = "bank1",
                        //    description = model.Content
                        //};
                        //HttpClient httpClient = new HttpClient();
                        //HttpRequestMessage request = new HttpRequestMessage();
                        //var httpContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                        //DateTime datetime = DateTime.Now;
                        //long time = ((DateTimeOffset)datetime).ToUnixTimeSeconds();
                        //var hashtring = Helpers.SecretKey_Partner + $"/api/transaction/addmoney" + data.sendPayAccount + data.sendAccountName + data.receiverPayAccount + data.typeFee + data.amountOwed + data.bankReferenceId + time.ToString();
                        //var token = Helpers.GetTokenOfPartner(hashtring);
                        //request.RequestUri = new Uri(Helpers.url_Partner + $"api/transaction/addmoney");
                        //request.Content = httpContent;
                        //request.Method = HttpMethod.Post;
                        //request.Headers.Add("signature", model.RSA);
                        //request.Headers.Add("token", token);
                        //request.Headers.Add("time", time.ToString());

                        //HttpResponseMessage response = await httpClient.SendAsync(request);
                        //if (response.StatusCode == HttpStatusCode.OK)
                        //{
                        //    _trans.Commit();
                        //    return true;
                        //}
                        return transaction.Id;
                    }
                    return 0;
                }
                return 0;
            }
            catch (Exception)
            {
                _trans.RollbackToSavepoint("BeforeTransfer");
                return 0;
            }
        }

        /// <summary>
        /// Nhận tiền chuyển khoản từ bên ngoài
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ReceiveExternalTransfer(ExternalTransfer model)
        {
            using var _trans = dbContext.Database.BeginTransaction();
            try
            {
                _trans.CreateSavepoint("BeforeTransfer");
                if (model.Send_STK != string.Empty && model.Send_Money > 0 && model.Receive_BankID > 0 && model.Receive_STK != String.Empty)
                {
                    var send_acc = dbContext.UserManages.Where(x => x.Stk == model.Receive_STK).FirstOrDefault();

                    if (send_acc != null)
                    {
                        var transaction = new TransactionBanking
                        {
                            Stksend = model.Send_STK,
                            Stkreceive = model.Receive_STK,
                            BankReferenceId = model.Receive_BankID,
                            Content = model.Content,
                            Money = model.Send_Money,
                            TransactionTypeId = 2,
                            PaymentFeeTypeId = model.PaymentFeeTypeID,
                            CreatedDate = DateTime.Now,
                            IsDebtRemind = false,
                            Rsa = model.RSA
                        };
                        dbContext.TransactionBankings.Add(transaction);
                        send_acc.SoDu = send_acc.SoDu + model.Send_Money;
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

        /// <summary>
        /// Xem lịch sử giao dịch của một tài khoản
        /// </summary>
        /// <param name="typeTransaction">type = 0: send transaction, type = 1: receive transaction, type = 2: debt transaction</param>
        /// <returns></returns>
        public List<TransactionVM> GetListTransactionByAcount(string accountNumber, int typeTransaction)
        {
            try
            {
                if (typeTransaction < 0 || typeTransaction > 2)
                    return null;

                return dbContext.TransactionBankings.Where(typeTransaction == 0 ? (trans => trans.Stksend == accountNumber) : (typeTransaction == 1 ? (trans => trans.Stkreceive == accountNumber) : (trans => trans.Stksend == accountNumber && trans.IsDebtRemind == true))).Where(x=> x.IsDeleted != true)
                            .Join(dbContext.TransactionTypes, d1 => d1.TransactionTypeId, d2 => d2.Id, (d1, d2) => new { d1.Id, d1.Stkreceive, d1.Stksend, d1.Content, d1.Money, d1.PaymentFeeTypeId, d1.BankReferenceId, TransactionTypeName = d2.Name, TransDate = d1.CreatedDate, IsDebtRemind = d1.IsDebtRemind })
                            .Join(dbContext.PaymentFeeTypes, d1 => d1.PaymentFeeTypeId, d2 => d2.Id, (d1, d2) => new { d1.Id, d1.Stkreceive, d1.Stksend, d1.Content, d1.Money, d1.TransactionTypeName, PaymentFeeTypeName = d2.Name, d1.BankReferenceId, d1.TransDate, d1.IsDebtRemind })
                            .Join(dbContext.BankReferences, d1 => d1.BankReferenceId, d2 => d2.Id,
                            (d1, d2) => new TransactionVM()
                            {
                                Id = d1.Id,
                                STKReceive = d1.Stkreceive,
                                STKSend = d1.Stksend,
                                Content = d1.Content,
                                Money = d1.Money,
                                TransactionType = d1.TransactionTypeName,
                                PaymentFeeType = d1.PaymentFeeTypeName,
                                BankReference = d2.Name,
                                TransDate = d1.TransDate,
                                IsDebtRemind = d1.IsDebtRemind
                            }).OrderByDescending(trans => trans.TransDate).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }

        /// <summary>
        /// Xem thông tin của giao dịch
        /// </summary>
        /// <param name="transactionId">Id transaction</param>
        /// <returns></returns>
        public TransactionVM GetInforTransaction(int transactionId)
        {
            try
            {
                return dbContext.TransactionBankings.Where(x => x.Id == transactionId).Join(dbContext.UserManages, d1 => d1.Stksend, d2 => d2.Stk,
                    (d1, d2) => new TransactionVM() { Content = d1.Content, Money = d1.Money, STKReceive = d1.Stkreceive, STKSend = d1.Stksend, TransactionType = d2.Name, TransDate = d1.CreatedDate }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
