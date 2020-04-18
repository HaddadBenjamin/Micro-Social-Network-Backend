using AutoMapper;
using DiabloII.Application.Responses.Users;
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

            CreateMap<UserNotificationSetting, UserNotificationSettingDto>().AfterMap((dataModel, dto) =>
            {
                dto.AcceptedNotifications = EnumerationFlagsHelper.ToEnumerations<NotificationType>(dataModel.AcceptedNotifications);
                dto.AcceptedNotifiers = EnumerationFlagsHelper.ToEnumerations<NotifierType>(dataModel.AcceptedNotifiers);
            });

            CreateMap<UserNotification, UserNotificationDto>().AfterMap((dataModel, dto) =>
            {
                dto.Content = dataModel.Notification.Content;
                dto.Title = dataModel.Notification.Title;
                dto.Type = dataModel.Notification.Type;
            });
        }
    }
}