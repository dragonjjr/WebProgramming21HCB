using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace PrjectWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Đăng ký tài khoản
        /// </summary>
        /// <param name="accountViewModel">accountViewModel: Thông tin tài khoản đăng ký</param>
        /// <returns></returns>
        [HttpPost("RegisterAccount")]
        public ResponeseMessage RegisterAccount([FromBody] AccountViewModel accountViewModel)
        {
            ResponeseMessage rs = new ResponeseMessage();

            var model = _employeeService.RegisterAccount(accountViewModel);
            if (model != "0")
            {
                rs.Status = 200;
                rs.Message = "Register an account successfully!";
                rs.Data = model;
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Register an account failed!";
            }
            return rs;
        }

        /// <summary>
        /// Nạp tiền vào tài khoản
        /// </summary>
        /// <param name="rechargeInput">rechargeInput: Thông tin nạp tiền vào tài khoản</param>
        /// <returns></returns>
        [HttpPost("Recharge")]
        public ResponeseMessage Recharge([FromBody] RechargeInput rechargeInput)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _employeeService.Recharge(rechargeInput);
            if (model > 0)
            {
                rs.Status = 200;
                rs.Message = "Recharge successfully!";
                rs.Data = model;
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Recharge failed!";
            }
            return rs;
        }

        /// <summary>
        /// Xem thông tin tài khoản
        /// </summary>
        /// <param name="infor">BankID: Id của ngân hàng; STK: số tk cần xem thông tin</param>
        /// <returns></returns>
        [HttpPost("GetAccountInfor")]
        public ResponeseMessage GetAccountInfor([FromBody] AccountInforInput infor)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _employeeService.GetAccountInfor(infor);
            if (model != null)
            {
                rs.Status = 200;
                rs.Message = "Get Account infor successfully!";
                rs.Data = model;
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Get Account infor failed!";
            }
            return rs;
        }
    }
}
