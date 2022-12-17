using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace PrjectWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService _notificationService)
        {
            this._notificationService = _notificationService;
        }

        /// <summary>
        /// API Get list notification of a account
        /// </summary>
        /// <param name="STKReceive"></param>
        /// <returns></returns>
        /// 
        [HttpGet("GetNotifications")]
        public ResponeseMessage GetNotifications(string STKReceive)
        {
            ResponeseMessage rs = new ResponeseMessage();
            var model = _notificationService.GetNotifications(STKReceive);
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

      
    }
}
