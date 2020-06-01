using AutoMapper;
using DiabloII.Application.Responses.Read.Domains.Notifications;
using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Application.Mappers.Notifications
{
    public class NotificationDataToDtoLayer : Profile
    {
        public NotificationDataToDtoLayer()
        {
            CreateMap<Notification, NotificationDto>();
        }
    }
}