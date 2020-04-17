using DiabloII.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Infrastructure.DbContext.Mappers.Users
{
    public static class UserNotificationMapper
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var userNotificationBuilder = modelBuilder.Entity<UserNotification>();

            userNotificationBuilder.HasKey(userNotification => userNotification.Id);
            userNotificationBuilder
                .HasIndex(userNotification => userNotification.Id)
                .IsUnique();

            userNotificationBuilder
                .HasOne(userNotification => userNotification.Notification)
                .WithMany(notification => notification.UserNotifications)
                .HasForeignKey(userNotification => userNotification.NotificationId)
                .IsRequired();

            userNotificationBuilder
                .HasOne(userNotification => userNotification.UserNotificationSetting)
                .WithMany(userNotificationSetting => userNotificationSetting.UserNotifications)
                .HasForeignKey(userNotification => userNotification.UserNotificationSettingId)
                .IsRequired();
        }
    }
}