using System;
using DiabloII.Application.Responses.Suggestions;
using Halcyon.HAL;

namespace DiabloII.Application.Services.Hals.Suggestions
{
    public interface ISuggestionHalService : IHalService<SuggestionDto>
    {
        HALResponse AddLinks(SuggestionCommentDto comment, Guid suggestionId);
    }
}