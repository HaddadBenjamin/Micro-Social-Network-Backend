using System.Collections.Generic;
using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Domain.Repositories
{
    public interface INotificationRepository
    {
        #region Read
        IReadOnlyCollection<Notification> GetAll();
        #endregion
    }
}