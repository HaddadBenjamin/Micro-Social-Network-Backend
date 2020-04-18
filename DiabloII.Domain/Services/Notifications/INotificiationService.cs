using System.Collections.Generic;
using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Domain.Services.Notifications
{

    public interface INotificationService
    {
        IList<INotifier> Notifiers { get; }

        void Notify(Notification notification, IReadOnlyCollection<string> concernedUserIds = null);
    }
}