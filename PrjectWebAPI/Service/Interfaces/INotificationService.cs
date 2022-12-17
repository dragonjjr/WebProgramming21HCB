using Common;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface INotificationService
    {
        List<NotificationInfo> GetNotifications(string STKReceive);
    }
}
