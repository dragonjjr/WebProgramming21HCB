using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace PrjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("test")]
        public ResponeseMessage RegisterAccount([FromBody] AccountViewModel accountViewModel)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _employeeService.RegisterAccount(accountViewModel);
            if (model)
            {
                rs.Status = 200;
                rs.Message = "Register an account successfully!";
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Register an account failed!";
            }
            return rs;
        }

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
