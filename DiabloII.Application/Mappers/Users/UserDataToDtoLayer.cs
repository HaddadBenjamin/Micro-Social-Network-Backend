using AutoMapper;
using DiabloII.Application.Responses.Read.Users;
using DiabloII.Domain.Extensions;
using DiabloII.Domain.Helpers;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Models.Users;

namespace DiabloII.Application.Mappers.Users
{
    public class UserDataToDtoLayer : Profile
    {
        public UserDataToDtoLayer()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserNotificationSetting, UserNotificationSettingDto>()
                .Ignore(dto => dto.AcceptedNotifications)
                .Ignore(dto => dto.AcceptedNotifiers)
                .AfterMap((dataModel, dto) =>
            {
                dto.AcceptedNotifications = EnumerationFlagsHelpers.ToStrings<NotificationType>(dataModel.AcceptedNotifications);
                dto.AcceptedNotifiers = EnumerationFlagsHelpers.ToStrings<NotifierType>(dataModel.AcceptedNotifiers);
            });

            CreateMap<UserNotification, UserNotificationDto>().AfterMap((dataModel, dto) =>
            {
                dto.Content = dataModel.Notification.Content;
                dto.Title = dataModel.Notification.Title;
                dto.Type = dataModel.Notification.Type.ToString();
            });
        }
    }
}