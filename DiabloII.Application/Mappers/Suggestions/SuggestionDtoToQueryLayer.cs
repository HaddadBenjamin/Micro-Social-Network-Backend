using AutoMapper;
using DiabloII.Application.Requests.Read.Domains.Suggestions;
using DiabloII.Domain.Queries.Domains.Suggestions;

namespace DiabloII.Application.Mappers.Suggestions
{
    public class SuggestionDtoToQueryLayer : Profile
    {
        public SuggestionDtoToQueryLayer() => CreateMap<GetASuggestionDto, GetASuggestionQuery>();
    }
}