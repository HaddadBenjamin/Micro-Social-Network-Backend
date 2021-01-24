using System;
using DiabloII.Application.Hals.Bases;
using DiabloII.Application.Responses.Read.Suggestions;
using Halcyon.HAL;

namespace DiabloII.Application.Hals.Domains.Suggestions.Decorators
{
    public interface ISuggestionHalDecorator : IHalDecorator<SuggestionDto>
    {
        HALResponse AddLinks(SuggestionCommentDto comment, Guid suggestionId);
    }
}