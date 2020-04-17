using DiabloII.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Infrastructure.DbContext.Mappers.Users
{
    public static class UserMapper
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            var userBuilder = modelBuilder.Entity<User>();

            userBuilder.HasKey(user => user.Id);
            userBuilder
                .HasIndex(user => user.Id)
                .IsUnique();

            userBuilder
                .Property(user => user.Email)
                .IsRequired();

            userBuilder
                .HasOne(user => user.NotificationSetting)
                .WithOne(userNotificationSetting => userNotificationSetting.User)
                .IsRequired();
        }
    }
}