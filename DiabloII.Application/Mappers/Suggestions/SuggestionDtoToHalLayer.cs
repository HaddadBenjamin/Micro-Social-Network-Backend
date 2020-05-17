using AutoMapper;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Domain.Extensions;

namespace DiabloII.Application.Mappers.Suggestions
{
    public class SuggestionDtoToHalLayer : Profile
    {
        public SuggestionDtoToHalLayer() => CreateMap<SuggestionDto, SuggestionHalResponse>()
            .Ignore(dto => dto.Votes)
            .Ignore(dto => dto.Comments);
    }
}