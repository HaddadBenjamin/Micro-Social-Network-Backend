using System;
using DiabloII.Application.Responses.Read.Suggestions;
using Halcyon.HAL;

namespace DiabloII.Application.Services.Hals.Suggestions
{
    public interface ISuggestionHalService : IHalService<SuggestionDto>
    {
        HALResponse AddLinks(SuggestionCommentDto comment, Guid suggestionId);
    }
}