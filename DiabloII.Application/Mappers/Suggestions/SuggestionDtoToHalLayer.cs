using System.Linq;
using AutoMapper;
using DiabloII.Application.Responses.Read.Suggestions;
using DiabloII.Application.Services.Hals.Domains.Suggestions;
using DiabloII.Domain.Extensions;

namespace DiabloII.Application.Mappers.Suggestions
{
    public static class SuggestionDtoToHalLayer
    {
        /// TODO : simplify this part by finding a way to resolve dependencies in automapper profile
        public static SuggestionHalResponse Map(SuggestionDto suggestion, ISuggestionHalService suggestionHalService)
        {
            var mapper = new MapperConfiguration(configuration => configuration
                .CreateMap<SuggestionDto, SuggestionHalResponse>()
                .Ignore(suggestion => suggestion.Comments)
                .AfterMap((dto, suggestionHalResponse) =>
                {
                    suggestionHalResponse.Comments = suggestion.Comments.Select(comment => suggestionHalService.AddLinks(comment, suggestion.Id)).ToList();
                })
            ).CreateMapper();
            var suggestionHalResponse = mapper.Map<SuggestionHalResponse>(suggestion);

            return suggestionHalResponse;
        }
    }
}