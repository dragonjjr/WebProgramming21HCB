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
        public ExternalController(IInternalTransferService internalTransferService)
        {
            _internalTransferService = internalTransferService;
        }

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

        [HttpPost("SendMoney")]
        public async Task<ResponeseMessage> SendMoney(SendMoneyRequest input)
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
                Receive_BankID = 1,
                Receive_STK = input.receiverPayAccount,
                Content = input.description,
                PaymentFeeTypeID = paymentfee,
                TransactionTypeID = 1,
                BankReferenceId = 2,
                RSA = signature,
            };
            var rs = await _internalTransferService.ExternalTransfer(model);
            if (rs)
            {
                result.Status = 200;
                result.Message = "Send Money successfull";
            }
            return result;
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
        /// <param name="model"></param>
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
