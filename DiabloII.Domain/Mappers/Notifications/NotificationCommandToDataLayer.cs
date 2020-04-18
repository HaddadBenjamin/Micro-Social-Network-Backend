using System;
using AutoMapper;
using DiabloII.Domain.Commands.Notifications;
using DiabloII.Domain.Models.Notifications;

namespace DiabloII.Domain.Mappers.Notifications
{
    public class NotificationCommandToDataLayer : Profile
    {
        public NotificationCommandToDataLayer()
        {
            CreateMap<CreateANotificationCommand, Notification>().AfterMap((command, dataModel) =>
            {
                dataModel.Id = Guid.NewGuid();
            });
        }
    }
}
