using System;
using System.Collections.Generic;
using System.Linq;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Repositories.Domains;
using DiabloII.Infrastructure.DbContext;

namespace DiabloII.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public NotificationRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        #region Read
        public IReadOnlyCollection<Notification> GetAll() => _dbContext.Notifications.ToList();

        public Notification Get(Guid notificationId) => _dbContext.Notifications.SingleOrDefault(n => n.Id == notificationId);
        #endregion
    }
}