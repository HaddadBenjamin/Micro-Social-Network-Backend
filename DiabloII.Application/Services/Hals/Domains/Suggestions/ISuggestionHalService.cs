using System;
using DiabloII.Application.Responses.Read.Suggestions;
using DiabloII.Application.Services.Hals.Bases;
using Halcyon.HAL;

namespace DiabloII.Application.Services.Hals.Domains.Suggestions
{
    public interface ISuggestionHalService : IHalService<SuggestionDto>
    {
        HALResponse AddLinks(SuggestionCommentDto comment, Guid suggestionId);
    }
}