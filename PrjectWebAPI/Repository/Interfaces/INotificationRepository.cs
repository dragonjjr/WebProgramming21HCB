﻿using Common;
using Repository.DBContext;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface INotificationRepository
    {
        List<NotificationInfo> GetNotifications(string STKReceive);
    }
}
