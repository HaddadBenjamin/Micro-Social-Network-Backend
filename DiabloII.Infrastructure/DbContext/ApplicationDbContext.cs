using DiabloII.Domain.Models.ErrorLogs;
using DiabloII.Domain.Models.Items;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Models.Users;
using DiabloII.Infrastructure.DbContext.Mappers.Items;
using DiabloII.Infrastructure.DbContext.Mappers.Notifications;
using DiabloII.Infrastructure.DbContext.Mappers.Suggestions;
using DiabloII.Infrastructure.DbContext.Mappers.Users;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Infrastructure.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Suggestion> Suggestions { get; set; }

        public DbSet<SuggestionVote> SuggestionVotes { get; set; }

        public DbSet<SuggestionComment> SuggestionComments { get; set; }

        public DbSet<ErrorLog> ErrorLogs { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<ItemProperty> ItemProperties { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserNotificationSetting> UserNotificationSettings { get; set; }

        public DbSet<UserNotification> UserNotifications { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SuggestionMapper.Map(modelBuilder);
            SuggestionVoteMapper.Map(modelBuilder);
            SuggestionCommentMapper.Map(modelBuilder);

            ItemMapper.Map(modelBuilder);
            ItemPropertyMapper.Map(modelBuilder);

            NotificationMapper.Map(modelBuilder);

            UserMapper.Map(modelBuilder);
            UserNotificationSettingMapper.Map(modelBuilder);
            UserNotificationMapper.Map(modelBuilder);
        }
    }
}
