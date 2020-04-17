using System;
using DiabloII.Domain.Models.Notifications;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Infrastructure.DbContext.Mappers.Notifications
{
    public static class NotificationMapper
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var notificationBuilder = modelBuilder.Entity<Notification>();

            notificationBuilder.HasKey(notification => notification.Id);
            notificationBuilder
                .HasIndex(notification => notification.Id)
                .IsUnique();

            notificationBuilder
                .Property(notification => notification.Type)
                .HasConversion(
                    notificationType => notificationType.ToString(),
                    notificationType => (NotificationType)Enum.Parse(typeof(NotificationType), notificationType));

            notificationBuilder
                .Property(notification => notification.Title)
                .IsRequired();

            notificationBuilder
                .Property(notification => notification.Content)
                .IsRequired();

            notificationBuilder
                .HasMany(notification => notification.UserNotifications)
                .WithOne(userNotification => userNotification.Notification)
                .HasForeignKey(userNotification => userNotification.NotificationId)
                .IsRequired();
        }
    }
}