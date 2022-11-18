using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Common.Helpers;

namespace PrjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("ForgotPassword")]
        public ResponeseMessage SendMail([FromBody]EmailInput emailInput)
        {
            ResponeseMessage rs = new ResponeseMessage();

            bool model = _emailService.SendMailForAccount(emailInput.Email);
            if (model)
            {
                rs.Status = 200;
                rs.Message = "Send otp code successfully!";
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Send otp code failed!";
            }
            return rs;
        }
    }
}
