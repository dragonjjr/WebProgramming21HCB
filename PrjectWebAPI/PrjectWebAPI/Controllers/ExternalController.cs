using Common;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using Org.BouncyCastle.Bcpg;
using Repository.DBContext;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace External.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalController : ControllerBase
    {
        private readonly IInternalTransferService _internalTransferService;
        private readonly IEmailService _emailService;
        public ExternalController(IInternalTransferService internalTransferService, IEmailService emailService)
        {
            _internalTransferService = internalTransferService;
            _emailService = emailService;
        }

        /// <summary>
        /// Xem thông tin tài khoản đối tác theo STK
        /// </summary>
        /// <param name="STK">Số tài khoản từ ngân hàng liên kết cần xem thông tin</param>
        /// <returns></returns>
        [HttpGet("GetInforFromPartner")]
        public async Task<ResponeseMessagePartner> GetInforFromPartner(string STK)
        {
            var result = new ResponeseMessagePartner();
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(Helpers.url_Partner + $"api/getInfo/{STK}");
            request.Method = HttpMethod.Get;
            DateTime datetime = DateTime.Now;
            long time = 16711357956;//((DateTimeOffset)datetime).ToUnixTimeSeconds();
            var hashtring = Helpers.SecretKey_Partner + $"/api/getInfo/{STK}" + time.ToString();
            var token = Helpers.GetTokenOfPartner(hashtring);
            request.Headers.Add("token", token);
            request.Headers.Add("time", time.ToString());
            HttpResponseMessage response = await httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            if(response.StatusCode == HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<ResponeseMessagePartner>(responseString);
            }    
            return result;
        }

        /// <summary>
        /// Chuyển tiền liên ngân hàng
        /// </summary>
        /// <param name="input">sendPayAccount: STK chuyển tiền;
        /// sendAccountName: Tên chủ tài khoản chuyển tiền; 
        /// receiverPayAccount: TK nhận tiền;
        /// receiverPayAccount: TK nhận tiền;
        /// typeFee: "receiver": người nhận trả phí, "sender": người chuyển trả phí;
        /// amountOwed: Số tiền cần chuyển;
        /// bankReferenceId: Thông tin ngân hàng tham chiếu. Mặc định với ngân hàng liên kết là "bank1";
        /// description: Mô tả chuyển tiền</param>
        /// <returns></returns>
        [HttpPost("SendMoney")]
        public ResponeseMessage SendMoney(SendMoneyRequest input)
        {
            var result = new ResponeseMessage();
            var signature = Helpers.EncryptionPartner(Helpers.Signature);
            int paymentfee = 1;
            if(input.typeFee == "receiver")
            {
                paymentfee = 2;
            }
            else
            {
                paymentfee = 1;
            }
            ExternalTransfer model = new ExternalTransfer
            {
                Send_STK = input.sendPayAccount,
                Send_Money = input.amountOwed,
                Receive_BankID = 2,
                Receive_STK = input.receiverPayAccount,
                Content = input.description,
                PaymentFeeTypeID = paymentfee,
                TransactionTypeID = 2,
                BankReferenceId = 2,
                RSA = signature,
            };
            var rs =  _internalTransferService.ExternalTransfer(model);
            if (rs > 0)
            {
                var sent = _emailService.SendMailForTransaction(model.Send_STK, rs);
                result.Status = 200;
                result.Message = "Send Money successfull";
                result.Data = rs;
            }
            else
            {
                result.Status = 0;
                result.Message = "Send Money failed";
            }
            return result;
        }

        /// <summary>
        /// Lấy thông tin người nhận bằng STK
        /// </summary>
        /// <param name="STK">STK cần xem thông tin</param>
        /// <returns></returns>
        [HttpGet("ViewRecipientBySTK")]
        public ResponeseMessage ViewRecipientBySTK(string STK)
        {
            ResponeseMessage rs = new ResponeseMessage();
            if (!CheckToken())
            {
                rs.Status = 0;
                rs.Message = "Request failed!";
                return rs;
            }
            else
            {
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
            
        }

        /// <summary>
        /// Nhận tiền từ chuyển khoản liên ngân hàng
        /// </summary>
        /// <param name="model">Send_STK: STK chuyển tiền; 
        /// Send_Money: số tiền cần chuyển;
        /// Receive_BankID: Ngân hàng liên kết ID: mặc định liên kết là 2 
        /// Receive_STK: Số tài khoản nhận tiền
        /// Content: Nội dung chuyển tiền
        /// PaymentFeeTypeID: Phí chuyển tiền: 1 người chuyển trả phí, 2: người nhận trả phí
        /// TransactionTypeID: Loại chuyển khoản: 2 chuyển khoản liên ngân hàng
        /// BankReferenceId: để null
        /// RSA: Chuỗi RSA từ đối tác cung cấp để lưu vào DB phục vụ đối chiếu sau này</param>
        /// <returns></returns>
        [HttpPost("ExternalTranfer")]
        public ResponeseMessage ExternalTranfer(ExternalTransfer model)
        {
            ResponeseMessage rs = new ResponeseMessage();
            if (!CheckSignature(model.Send_STK, model.Send_Money.ToString(), model.Receive_STK))
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
                    rs.Data = Is_Success;
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

        private bool CheckSignature(string sendSTK, string sendMoney, string receiveSTK)
        {
            if (!Request.Headers.ContainsKey("Token"))
                return false;
            var header_signature = Request.Headers["Signature"];
            var header_token = Request.Headers["Token"];
            var header_time = Request.Headers["Time"];
            var en = Helpers.Encryption(sendSTK + sendMoney + receiveSTK);
            if (!string.IsNullOrEmpty(header_token) && !string.IsNullOrEmpty(header_time))
            {
                var token = Helpers.GetToken(header_time);
                var decode = Helpers.Decryption(header_signature);
                if (header_token == token && decode == (sendSTK+sendMoney+receiveSTK))
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
