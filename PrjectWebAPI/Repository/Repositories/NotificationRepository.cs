using Common;
using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly _6IVYVvfe0wContext dbContext;
        public NotificationRepository(_6IVYVvfe0wContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<NotificationInfo> GetNotifications(string STKReceive)
        {
            try
            {
                var rows = dbContext.Notifications.Where(notice => notice.Stkreceive == STKReceive && notice.IsDeleted == false).Select(notice => new NotificationInfo
                {
                    Id = notice.Id,
                    STKReceive = notice.Stkreceive,
                    STKSend = notice.Stksend,
                    Content = notice.Content
                }).ToList();

                return rows;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }

}
