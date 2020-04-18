using System;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Models.Users;

namespace DiabloII.Domain.Mappers.Users
{
    public static class UserCrossDomainMapper
    {
        public static UserNotification ToUserNotification(User user, Notification notification) => new UserNotification
        {
            Id = Guid.NewGuid(),
            HaveBeenRead = false,
            Notification = notification,
            NotificationId = notification.Id,
            UserNotificationSetting = user.NotificationSetting,
            UserNotificationSettingId = user.NotificationSetting.Id
        };
    }
}