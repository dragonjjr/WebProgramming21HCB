using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.DBContext;
using Service.Interfaces;
using Service.Services;
using System.Collections.Generic;

namespace PrjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternalTransferController : ControllerBase
    {
        private readonly IInternalTransferService _internalTransferService;
        public InternalTransferController(IInternalTransferService internalTransferService)
        {
            _internalTransferService = internalTransferService;
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
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("CheckOTPTransaction")]
        public ResponeseMessage CheckOTPTransaction(CheckOTPTransaction model)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var isvalid = _internalTransferService.CheckOTPTransaction(model);
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
        /// <param name="STK"></param>
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

        /// <summary>
        /// Lấy danh sách tài khoản thanh toán
        /// </summary>
        /// <param name="UserID"></param>
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
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("InternalTranfer")]
        public ResponeseMessage InternalTranfer(InternalTransfer model)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var Is_Success = _internalTransferService.InternalTransfer(model);
            if (Is_Success)
            {
                rs.Status = 200;
                rs.Message = "Transfer successfull!";
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Transfer failed!";
            }
            return rs;
        }

        /// <summary>
        /// Chuyển khoản liên ngân hàng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ExternalTranfer")]
        public ResponeseMessage ExternalTranfer(ExternalTransfer model)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var Is_Success = _internalTransferService.ExternalTransfer(model);
            if (Is_Success)
            {
                rs.Status = 200;
                rs.Message = "Transfer successfull!";
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Transfer failed!";
            }
            return rs;
        }
    }
}
