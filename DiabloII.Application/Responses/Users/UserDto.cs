﻿namespace DiabloII.Application.Responses.Users
{
    public class UserDto
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public UserNotificationSettingDto NotificationSetting { get; set; }
    }
}