using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.DBContext;
using Service.Interfaces;
using Service.Services;
using System.Collections.Generic;

namespace PrjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternalTransferController : ControllerBase
    {
        private readonly IInternalTransferService _internalTransferService;
        public InternalTransferController(IInternalTransferService internalTransferService)
        {
            _internalTransferService = internalTransferService;
        }

        [HttpGet("GetPaymentFeeType")]
        public ResponeseMessage GetPaymentFeeType()
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _internalTransferService.GetPaymentFeeType();
            if (model.Count > 0)
            {
                rs.Status = 200;
                rs.Message = "Get payment fee type successfully!";
                rs.Data = model;
            }
            else
            {
                rs.Status = 0;
                rs.Message = "Get payment fee type failed!";
            }
            return rs;
        }
    }
}
