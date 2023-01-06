using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;


namespace PrjectWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Thêm người thụ hưởng/ người nhận
        /// </summary>
        /// <param name="recipientInput">STK: Số tài khoản người thụ hưởng;
        /// Name: Tên người thụ hưởng</param>
        /// <returns></returns>
        [HttpPost("Recipient/AddRecipient")]
        public ResponeseMessage AddRecipient([FromBody] RecipientInput recipientInput)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _customerService.AddRecipient(recipientInput);
            if (model)
            {
                rs.Status = 200;
                rs.Message = "Add a recipient successfully!";
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Add a recipient failed!";
            }
            return rs;
        }

        /// <summary>
        /// Xem thông tin số tài khoản và số dư của khách hàng
        /// </summary>
        /// <param name="id">Id: id số tài khoản cần xem số dư</param>
        /// <returns></returns>
        [HttpGet("GetUserBalance/{id}")]
        public ResponeseMessage GetUserBalance(int id)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _customerService.GetUserBalance(id);
            if (model != null)
            {
                rs.Status = 200;
                rs.Message = "Get user balance successfully!";
                rs.Data = model;
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Get user balance failed!";
            }
            return rs;

        }

        /// <summary>
        /// Cập nhật người thụ hưởng/ người nhận
        /// </summary>
        /// <param name="id">Id: id cần cập nhật thông tin;
        /// recipientEdit: Thông tin cần cập nhật</param>
        /// <returns></returns>
        [HttpPatch("Recipient/{id}")]
        public ResponeseMessage UpdateRecipient(int id, [FromBody] RecipientEdit recipientEdit)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _customerService.UpdateRecipient(id, recipientEdit);
            if (model)
            {
                rs.Status = 200;
                rs.Message = "Update a recipient successfully!";
                rs.Data = model;
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Update a recipient failed!";
            }
            return rs;
        }


        /// <summary>
        /// Xóa người thụ hưởng/ người nhận
        /// </summary>
        /// <param name="id">Id: id người thụ hưởng cần xóa</param>
        /// <returns></returns>
        [HttpDelete("Recipient/{id}")]
        public ResponeseMessage DeleteRecipient(int id)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _customerService.DeleteRecipient(id);
            if (model)
            {
                rs.Status = 200;
                rs.Message = "Delete a recipient successfully!";
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Delete a recipient failed!";
            }
            return rs;
        }

        /// <summary>
        /// Lấy danh sách người nhận của một khách hàng
        /// </summary>
        /// <param name="id">Id: id khách hàng cần lấy danh sách người thụ hưởng</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ResponeseMessage GetListRecipientByUserId(int id)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _customerService.GetListRecipientByUserId(id);
            if (model != null)
            {
                rs.Status = 200;
                rs.Message = "Get list recipient successfully!";
                rs.Data = model;
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Get list recipient failed!";
            }
            return rs;

        }

        /// <summary>
        /// Lấy danh sách ngân hàng liên kết
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetListBankReference")]
        public ResponeseMessage GetListBankReference()
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _customerService.GetBankReferences();
            if (model != null)
            {
                rs.Status = 200;
                rs.Message = "Get list bank reference successfully!";
                rs.Data = model;
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Get list  bank reference failed!";
            }
            return rs;

        }
    }
}
