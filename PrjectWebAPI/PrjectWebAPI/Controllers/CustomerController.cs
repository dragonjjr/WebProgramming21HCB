using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;


namespace PrjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpPost("AddRecipient")]
        public ResponeseMessage AddRecipient([FromBody]RecipientInput recipientInput)
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
    }
}
