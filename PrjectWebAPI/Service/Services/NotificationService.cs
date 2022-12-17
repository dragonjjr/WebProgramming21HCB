using Common;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class NotificationService:INotificationService
    {
        private INotificationRepository _INotificationRepository;

        public NotificationService(INotificationRepository _INotificationRepository)
        {
            this._INotificationRepository = _INotificationRepository;
        }

        public List<NotificationInfo> GetNotifications(string STKReceive)
        {
            return this._INotificationRepository.GetNotifications(STKReceive);
        }
    }
}
