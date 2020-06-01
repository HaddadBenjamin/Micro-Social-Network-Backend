namespace DiabloII.Application.Responses.Read.Domains.Users
{
    public class UserDto
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public UserNotificationSettingDto NotificationSetting { get; set; }
    }
}