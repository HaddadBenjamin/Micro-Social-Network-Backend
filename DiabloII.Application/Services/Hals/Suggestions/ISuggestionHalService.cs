using DiabloII.Application.Responses.Suggestions;
using Halcyon.HAL;

namespace DiabloII.Application.Services.Hals.Suggestions
{
    public interface ISuggestionHalService
    {
        HALResponse AddLinks(SuggestionDto dto);
    }
}