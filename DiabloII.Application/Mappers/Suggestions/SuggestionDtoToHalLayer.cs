using System.Linq;
using AutoMapper;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Services.Hals.Suggestions;
using DiabloII.Domain.Extensions;

namespace DiabloII.Application.Mappers.Suggestions
{
    public static class SuggestionDtoToHalLayer
    {
        public static SuggestionHalResponse Map(SuggestionDto suggestion, ISuggestionHalService suggestionHalService)
        {
            var mapper = new MapperConfiguration(configuration => configuration
                .CreateMap<SuggestionDto, SuggestionHalResponse>()
                .Ignore(suggestion => suggestion.Votes)
                .Ignore(suggestion => suggestion.Comments)
                .AfterMap((dto, suggestionHalResponse) =>
                {
                    suggestionHalResponse.Votes = suggestion.Votes.Select(vote => suggestionHalService.AddLinks(vote, suggestion.Id)).ToList();
                    suggestionHalResponse.Comments = suggestion.Comments.Select(comment => suggestionHalService.AddLinks(comment, suggestion.Id)).ToList();
                })
            ).CreateMapper();
            var suggestionHalResponse = mapper.Map<SuggestionHalResponse>(suggestion);

            return suggestionHalResponse;
        }
    }
}