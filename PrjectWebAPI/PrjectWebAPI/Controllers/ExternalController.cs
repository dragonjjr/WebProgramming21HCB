using Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using Service.Interfaces;
using System.Net;
using System.Security.Claims;

namespace External.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalController : ControllerBase
    {
        private readonly IInternalTransferService _internalTransferService;
        public ExternalController(IInternalTransferService internalTransferService)
        {
            _internalTransferService = internalTransferService;
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
        /// Nhận tiền từ chuyển khoản liên ngân hàng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ExternalTranfer")]
        public ResponeseMessage ExternalTranfer(ExternalTransfer model)
        {
            ResponeseMessage rs = new ResponeseMessage();
            if (!CheckToken())
            {
                rs.Status = -1;
                rs.Message = "Request failed!";
                return rs;
            }
            else
            {
                var Is_Success = _internalTransferService.ReceiveExternalTransfer(model);
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
        private bool CheckToken()
        {
            if (!Request.Headers.ContainsKey("Token"))
                return false;
            var header_token = Request.Headers["Token"];
            var header_time = Request.Headers["Time"];

            if (!string.IsNullOrEmpty(header_token) && !string.IsNullOrEmpty(header_time))
            {
                var token = Helpers.GetToken(header_time);
                if (header_token == token)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
