using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjectWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;

        public AccountController(IAccountService accountService, IEmailService emailService)
        {
            _accountService = accountService;
            _emailService = emailService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ResponeseMessage Login([FromBody] AccountInput accountInput)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _accountService.Login(accountInput);
            if (model.LoggedIn)
            {
                rs.Status = 200;
                rs.Message = "Login successfully!";
                rs.Data = model;
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Login failed!";
            }
            return rs;
        }


        /// <summary>
        /// API Change password by Id
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        [HttpPatch("ChangePassword/{id}")]
        public ResponeseMessage ChangePassword(int id, [FromBody] ChangePasswordInput changePasswordInput)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _accountService.ChangePassword(id, changePasswordInput);
            if (model)
            {
                rs.Status = 200;
                rs.Message = "Change password successfully!";
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Change password failed!";
            }
            return rs;
        }


        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public ResponeseMessage SendMail([FromBody] EmailInput emailInput)
        {
            ResponeseMessage rs = new ResponeseMessage();

            bool model = _emailService.SendMailForAccount(emailInput.Email);
            if (model)
            {
                rs.Status = 200;
                rs.Message = "Send otp code successfully!";
                rs.Data = _accountService.GetAccountIdByEmail(emailInput.Email);
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Send otp code failed!";
            }
            return rs;
        }


        /// <summary>
        /// API Reset password by Id
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        [HttpPatch("ForgotPassword/ResetPassword/{id}")]
        public ResponeseMessage ResetPassword(int id, [FromBody] ResetPasswordInput resetPasswordInput)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _accountService.ResetPassword(id, resetPasswordInput);
            if (model.Status != 0)
            {
                rs.Status = 200;
                rs.Message = model.Message;
            }
            else
            {
                rs.Status = 0;
                rs.Message = model.Message;
            }
            return rs;
        }
    }
}
