using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.DBContext;
using Service.Interfaces;
using Service.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrjectWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InternalTransferController : ControllerBase
    {
        private readonly IInternalTransferService _internalTransferService;
        private readonly IEmailService _emailService;
        public InternalTransferController(IInternalTransferService internalTransferService, IEmailService emailService)
        {
            _internalTransferService = internalTransferService;
            _emailService = emailService;
        }

        /// <summary>
        /// Lấy danh sách hình thức thanh toán phí 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPaymentFeeType")]
        public ResponeseMessage GetPaymentFeeType()
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _internalTransferService.GetPaymentFeeType();
            if (model.Count > 0)
            {
                rs.Status = 200;
                rs.Message = "Get payment fee type successfully!";
                rs.Data = model;
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Get payment fee type failed!";
            }
            return rs;
        }

        /// <summary>
        /// Kiểm tra OTP có hợp lệ
        /// </summary>
        /// <param name="model">TransactionID: Id của giao dịch; OTP: mã code xác thực</param>
        /// <param name="isInternalTranfer">isInternalTranfer: true là kiểm tra otp giao dịch nội bộ, ngược lại là kiểm tra của giao dịch liên ngân hàng</param>
        /// <returns></returns>
        [HttpPost("CheckOTPTransaction/{isInternalTranfer}")]
        public async Task<ResponeseMessage> CheckOTPTransactionAsync(CheckOTPTransaction model, bool isInternalTranfer)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var isvalid = await _internalTransferService.CheckOTPTransaction(model, isInternalTranfer);
            if (isvalid)
            {
                rs.Status = 200;
                rs.Message = "OTP is Valid!";
            }
            else
            {
                rs.Status = 0;
                rs.Message = "OTP is inValid!";
            }
            return rs;
        }

        /// <summary>
        /// Lấy thông tin người nhận bằng STK
        /// </summary>
        /// <param name="STK">STK: STK cần xem thông tin</param>
        /// <returns></returns>
        [HttpGet("ViewRecipientBySTK")]
        public ResponeseMessage ViewRecipientBySTK(string STK)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _internalTransferService.ViewRecipientBySTK(STK);
            if (model != null)
            {
                rs.Status = 200;
                rs.Message = "Get recipient infor successfull!";
                rs.Data = model;
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Get recipient infor failed!";
            }
            return rs;
        }

        ///// <summary>
        ///// Nhận tiền từ chuyển khoản liên ngân hàng
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost("ExternalTranfer")]
        //public async Task<ResponeseMessage> ExternalTranfer(ExternalTransfer model)
        //{
        //    ResponeseMessage rs = new ResponeseMessage();
        //    var Is_Success = await _internalTransferService.ExternalTransfer(model);
        //    if (Is_Success)
        //    {
        //        rs.Status = 200;
        //        rs.Message = "Transfer successfull!";
        //    }
        //    else
        //    {
        //        rs.Status = 0;
        //        rs.Message = "Transfer failed!";
        //    }
        //    return rs;
        //}

        /// <summary>
        /// Lấy danh sách tài khoản thanh toán
        /// </summary>
        /// <param name="UserID">ID: id tài khoản cần lấy danh sách tài khoản thanh toán</param>
        /// <returns></returns>
        [HttpGet("GetListAccount")]
        public ResponeseMessage GetListAccount(int UserID)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _internalTransferService.GetListAccount(UserID);
            if (model != null)
            {
                rs.Status = 200;
                rs.Message = "Get list account successfull!";
                rs.Data = model;
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Get list account failed!";
            }
            return rs;
        }

        /// <summary>
        /// Chuyển khoản nội bộ
        /// </summary>
        /// <param name="model">model: thông tin chuyển khoản nội bộ</param>
        /// <returns></returns>
        [HttpPost("InternalTranfer")]
        public ResponeseMessage InternalTranfer(InternalTransfer model)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var Is_Success = _internalTransferService.InternalTransfer(model);
           
            if (Is_Success > 0)
            {
                var sent = _emailService.SendMailForTransaction(model.Send_STK,Is_Success);

                rs.Status = 200;
                rs.Message = "Transfer successfull!";
                rs.Data = Is_Success;
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Transfer failed!";
            }
            return rs;
        }

        /// <summary>
        /// Xem lịch sử giao dịch của một tài khoản
        /// </summary>
        /// <param name="typeTransaction">type = 0: send transaction, type = 1: receive transaction, type = 2: debt transaction</param>
        /// <returns></returns>
        [HttpGet("GetListTransactionByAcount")]
        public ResponeseMessage GetListTransactionByAcount(string accountNumber, int typeTransaction)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _internalTransferService.GetListTransactionByAcount(accountNumber, typeTransaction);
            if (model != null)
            {
                rs.Status = 200;
                rs.IsSuccess = true;
                rs.Message = "Get list transaction successfully!";
                rs.Data = model;
            }
            else
            {
                rs.Status = 0;
                rs.IsSuccess = false;
                rs.Message = "Get list transaction failed!";
            }
            return rs;
        }

        /// <summary>
        /// Xem thông tin của một giao dịch
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        [HttpGet("GetInforTransaction")]
        public ResponeseMessage GetInforTransactionByAcount(int transactionId)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _internalTransferService.GetInforTransaction(transactionId);
            if (model != null)
            {
                rs.Status = 200;
                rs.IsSuccess = true;
                rs.Message = "Get a transaction successfully!";
                rs.Data = model;
            }
            else
            {
                rs.Status = 0;
                rs.IsSuccess = false;
                rs.Message = "Get a transaction failed!";
            }
            return rs;
        }
    }
}
