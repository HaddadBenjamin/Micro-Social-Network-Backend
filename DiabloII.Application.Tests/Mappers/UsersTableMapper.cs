using System.Linq;
using DiabloII.Application.Requests.Users;
using DiabloII.Application.Responses.Read.Users;
using DiabloII.Domain.Helpers;
using DiabloII.Domain.Models.Notifications;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Mappers
{
    public static class UsersTableMapper
    {
        public static UpdateAUserDto ToUpdateAUserDto(TableRow row, string userId) => new UpdateAUserDto
        {
            UserId = userId,
            Email = row.GetString("Email"),
            AcceptedNotifications = row.GetString("AcceptedNotifications")
                ?.Split(",")
                .Select(enumerationString => EnumerationHelpers.ToEnumeration<NotificationType>(enumerationString)),
            AcceptedNotifiers = row.GetString("AcceptedNotifiers")
                ?.Split(",")
                .Select(enumerationString => EnumerationHelpers.ToEnumeration<NotifierType>(enumerationString)),
        };

        public static UserDto ToUserDto(TableRow row) => new UserDto
        {
            Id = row.GetString("UserId"),
            Email = row.GetString("Email"),
            NotificationSetting = new UserNotificationSettingDto
            {
                AcceptedNotifications = row.GetString("AcceptedNotifications")?.Split(","),
                AcceptedNotifiers = row.GetString("AcceptedNotifiers")?.Split(","),
            }
        };
    }
}