using System;
using AutoMapper;
using DiabloII.Application.Requests.Write.Notifications;
using DiabloII.Domain.Commands.Domains.Notifications;

namespace DiabloII.Application.Mappers.Notifications
{
    public class NotificationDtoToCommandLayer : Profile
    {
        public NotificationDtoToCommandLayer() =>
            CreateMap<CreateANotificationDto, CreateANotificationCommand>().AfterMap((dto, command) => command.Id = Guid.NewGuid());
    }
}
