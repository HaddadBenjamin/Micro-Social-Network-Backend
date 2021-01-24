using AutoMapper;
using DiabloII.Domain.Commands.Domains.Notifications;
using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Domain.Mappers.Notifications
{
    public class NotificationCommandToDataLayer : Profile
    {
        public NotificationCommandToDataLayer()
        {
            CreateMap<CreateANotificationCommand, Notification>();
        }
    }
}
