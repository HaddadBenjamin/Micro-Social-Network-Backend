using DiabloII.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Infrastructure.DbContext.Mappers.Users
{
    public static class UserNotificationSettingMapper
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var userNotificationSettingBuilder = modelBuilder.Entity<UserNotificationSetting>();

            userNotificationSettingBuilder.HasKey(userNotificationSetting => userNotificationSetting.Id);
            userNotificationSettingBuilder
                .HasIndex(userNotificationSetting => userNotificationSetting.Id)
                .IsUnique();

            userNotificationSettingBuilder
                .HasOne(userNotificationSetting => userNotificationSetting.User)
                .WithOne(user => user.NotificationSetting)
                .HasForeignKey<UserNotificationSetting>(userNotificationSetting => userNotificationSetting.UserId)
                .IsRequired();

            userNotificationSettingBuilder
                .HasMany(userNotificationSetting => userNotificationSetting.UserNotifications)
                .WithOne(userNotification => userNotification.UserNotificationSetting)
                .HasForeignKey(userNotification => userNotification.UserNotificationSettingId)
                .IsRequired();
        }
    }
}