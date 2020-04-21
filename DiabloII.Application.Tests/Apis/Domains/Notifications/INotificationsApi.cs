﻿using DiabloII.Application.Requests.Notifications;
using DiabloII.Application.Responses.Notifications;
using DiabloII.Application.Tests.Apis.Bases;

namespace DiabloII.Application.Tests.Apis.Domains.Notifications
{
    public interface INotificationsApi :
        IGetAllApi<NotificationDto>,
        ICreateApi<CreateANotificationDto, NotificationDto>
    {
    }
}