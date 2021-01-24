using System;
using AutoMapper;
using DiabloII.Domain.Queries.Domains.Suggestions;

namespace DiabloII.Application.Mappers.Notifications
{
    public class NotificationDtoToQueryLayer : Profile
    {
        public NotificationDtoToQueryLayer() =>
            CreateMap<Guid, GetNotificationQuery>()
                .AfterMap((dto, query) => query.Id = dto);
    }
}