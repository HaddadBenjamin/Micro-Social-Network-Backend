using AutoMapper;
using DiabloII.Application.Responses.Users;
using DiabloII.Domain.Models.Users;

namespace DiabloII.Application.Mappers.Users
{
    public class UserDataToDtoLayer : Profile
    {
        public UserDataToDtoLayer()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserNotificationSetting, UserNotificationSettingDto>();
            CreateMap<UserNotification, UserNotificationDto>().AfterMap((dataModel, dto) =>
            {
                dto.Content = dataModel.Notification.Content;
                dto.Title = dataModel.Notification.Title;
                dto.Type = dataModel.Notification.Type;
            });
        }
    }
}