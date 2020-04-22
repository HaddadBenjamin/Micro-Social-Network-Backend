using System.Collections.Generic;
using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Domain.Services.Notifications
{

    public interface INotificationService
    {
        IReadOnlyCollection<INotifier> Notifiers { get; }

        void Notify(Notification notification, IReadOnlyCollection<string> concernedUserIds = null);
    }
}