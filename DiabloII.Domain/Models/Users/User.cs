using DiabloII.Domain.Commands.Users;

namespace DiabloII.Domain.Models.Users
{
    public class User
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public UserNotificationSetting NotificationSetting { get; set; }

        public void Update(UpdateAUserCommand command)
        {
            Email = command.Email;
            NotificationSetting.AcceptedNotifications = command.AcceptedNotifications;
            NotificationSetting.AcceptedNotifiers = command.AcceptedNotifiers;
        }
    }
}
