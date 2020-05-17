using System;
using DiabloII.Application.Responses.Suggestions;
using Halcyon.HAL;

namespace DiabloII.Application.Services.Hals.Suggestions
{
    public interface ISuggestionHalService : IHalService<SuggestionDto>
    {
        HALResponse AddLinks(SuggestionVoteDto vote, Guid suggestionId);

        HALResponse AddLinks(SuggestionCommentDto comment, Guid suggestionId);
    }
}