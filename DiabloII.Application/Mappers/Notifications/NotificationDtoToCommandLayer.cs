using AutoMapper;
using DiabloII.Application.Requests.Write.Notifications;
using DiabloII.Domain.Commands.Notifications;

namespace DiabloII.Application.Mappers.Notifications
{
    public class NotificationDtoToCommandLayer : Profile
    {
        public NotificationDtoToCommandLayer()
        {
            CreateMap<CreateANotificationDto, CreateANotificationCommand>();
        }
    }
}
