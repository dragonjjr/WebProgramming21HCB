using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace PrjectWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorController(IAdministratorService _administratorService)
        {
            this._administratorService = _administratorService;
        }

        /// <summary>
        /// API Get list of employees by search condition (employee name). If search condition is empty, get all employees.
        /// </summary>
        /// <param name="searchName">searchName: tên tài khoản cần tìm</param>
        /// <returns></returns>
        /// 
        [HttpGet("GetEmployeeList")]
        public ResponeseMessage GetEmployeeList(string searchName)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _administratorService.GetListEmployee(searchName);
            if (model != null)
            {
                rs.Status = 200;
                rs.IsSuccess = true;
                rs.Message = "Get employee list successfully!";
                rs.Data = model;
            }
            else
            {
                rs.IsSuccess = false;
                rs.Status = 0;
                rs.Message = "Get employee list failed!";
            }
            return rs;

        }

        /// <summary>
        /// API Find employee by id
        /// </summary>
        /// <param name="id">Id tài khoản cần tìm</param>
        /// <returns></returns>
        [HttpGet("GetEmployee/{id}")]
        public ResponeseMessage FindEmployeeById(int id)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _administratorService.FindEmployeeById(id);
            if (model != null)
            {
                rs.Status = 200;
                rs.IsSuccess = true;
                rs.Message = "Find employee successfully!";
                rs.Data = model;
            }
            else
            {
                rs.IsSuccess = false;
                rs.Status = 0;
                rs.Message = "Find employee failed!";
            }
            return rs;

        }

        /// <summary>
        /// API Create a new employee information
        /// </summary>
        /// <param name="employeeInfo">UserName: Tài khoản đăng nhập;
        /// Password: Mật khẩu;
        /// Name: Tên;
        /// Cmnd: CMND/CCCD;
        /// Address: Địa chỉ;
        /// Email: Email;
        /// Phone: Số điện thoại</param>
        /// <returns></returns>
        [HttpPost("AddNewEmployee")]
        public ResponeseMessage AddNewEmployee([FromBody] EmployeeAccountInput employeeInfo)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var isSuccess = _administratorService.AddNewEmployee(employeeInfo);
            if (isSuccess)
            {
                rs.Status = 200;
                rs.IsSuccess = true;
                rs.Message = "Create a new employee successfully!";
            }
            else
            {
                rs.IsSuccess = false;
                rs.Status = 0;
                rs.Message = "Create a new employee failed!";
            }
            return rs;

        }

        /// <summary>
        /// API Update a employee information
        /// <param name="employeeId">employeeId: Id tài khoản cần cập nhật;
        /// employeeInfo: Những thông tin cần cập nhật</param>
        /// </summary>
        /// <returns></returns>
        [HttpPatch("UpdateEmployeeInfo/{employeeId}")]
        public ResponeseMessage UpdateEmployeeInfo(int employeeId, [FromBody] EmployeeInfoInput employeeInfo)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var isSuccess = _administratorService.UpdateEmployeeInfo(employeeId,employeeInfo);
            if (isSuccess)
            {
                rs.Status = 200;
                rs.IsSuccess = true;
                rs.Message = "Update a employee information successfully!";
            }
            else
            {
                rs.IsSuccess = false;
                rs.Status = 0;
                rs.Message = "Update a employee information failed!";
            }
            return rs;
        }

        /// <summary>
        /// API Delete a employee information
        /// </summary>
        /// <param name="employeeId">ID: nhân viên cần xóa</param>
        /// <returns></returns>
        [HttpDelete("DeleteEmployee/{employeeId}")]
        public ResponeseMessage DeleteEmployee(int employeeId)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var isSuccess = _administratorService.DeleteEmployee(employeeId);
            if (isSuccess)
            {
                rs.Status = 200;
                rs.IsSuccess = true;
                rs.Message = "Delete a employee information successfully!";
            }
            else
            {
                rs.IsSuccess = false;
                rs.Status = 0;
                rs.Message = "Delete a employee information failed!";
            }
            return rs;

        }

        /// <summary>
        /// API Get List Transaction Banking All Customer
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("ViewTransaction")]
        public ResponeseMessage GetListTransaction(int month, int year)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _administratorService.GetListTransaction(month,year);
            if (model != null)
            {
                rs.Status = 200;
                rs.Message = "Get list transaction successfully!";
                rs.Data = model;
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Get list transaction failed!";
            }
            return rs;
        }
    }
}
