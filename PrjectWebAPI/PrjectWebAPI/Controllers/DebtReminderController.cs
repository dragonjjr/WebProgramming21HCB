using Common;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
namespace PrjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtReminderController : ControllerBase
    {
        private readonly IDebtReminderService _debtReminderService;

        public DebtReminderController(IDebtReminderService _debtReminderService)
        {
            this._debtReminderService = _debtReminderService;
        }

        /// <summary>
        /// API Create a new debt remind
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        /// 
        [HttpPost("CreateDebtRemind")]
        public ResponeseMessage CreateDebtRemind([FromBody] DebtRemindInput debtRemindInfo)
        {
            ResponeseMessage rs = new ResponeseMessage();
            bool isSuccess = _debtReminderService.CreateDebtRemind(debtRemindInfo);
            if (isSuccess)
            {
                rs.Status = 200;
                rs.IsSuccess = true;
                rs.Message = "Create a new debt remind successfully!";
            }
            else
            {
                rs.IsSuccess = false;
                rs.Status = 0;
                rs.Message = "Create a new debt remind failed!";
            }
            return rs;

        }

        /// <summary>
        /// API view list debt remind
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("viewInfoDebtReminds/{STK}")]
        public ResponeseMessage viewInfoDebtReminds(string STK, bool isSelf, int? status)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _debtReminderService.viewInfoDebtReminds(STK, isSelf, status);
            if (model!=null)
            {
                rs.Status = 200;
                rs.IsSuccess = true;
                rs.Message = "Get list debt remind successfully!";
                rs.Data = model;
            }
            else
            {
                rs.IsSuccess = false;
                rs.Status = 0;
                rs.Message = "Get list debt remind failed!";
            }
            return rs;

        }

        /// <summary>
        /// API cancel a debt remind
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPatch("CancelDebtRemind/{debtRemindID}")]
        public ResponeseMessage CancelDebtRemind(int debtRemindID)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var isSuccess = _debtReminderService.CancelDebtRemind(debtRemindID);
            if (isSuccess)
            {
                rs.Status = 200;
                rs.IsSuccess = true;
                rs.Message = "Cancel debt remind successfully!";
            }
            else
            {
                rs.IsSuccess = false;
                rs.Status = 0;
                rs.Message = "Cancel debt remind failed!";
            }
            return rs;

        }

        /// <summary>
        /// API pay a debt remind
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPatch("payDebtRemind/{debtRemindID}")]
        public ResponeseMessage payDebtRemind(int debtRemindID)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var isSuccess = _debtReminderService.payDebtRemind(debtRemindID);
            if (isSuccess)
            {
                rs.Status = 200;
                rs.IsSuccess = true;
                rs.Message = "Payment success!";
            }
            else
            {
                rs.IsSuccess = false;
                rs.Status = 0;
                rs.Message = "Payment fail!";
            }
            return rs;

        }
    }
}
