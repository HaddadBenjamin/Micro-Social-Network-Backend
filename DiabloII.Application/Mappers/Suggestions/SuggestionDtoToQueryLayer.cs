using System;
using AutoMapper;
using DiabloII.Domain.Queries.Domains.Notifications;

namespace DiabloII.Application.Mappers.Suggestions
{
    public class SuggestionDtoToQueryLayer : Profile
    {
        public SuggestionDtoToQueryLayer() =>
            CreateMap<Guid, GetSuggestionQuery>()
                .AfterMap((dto, query) => query.Id = dto);
    }
}