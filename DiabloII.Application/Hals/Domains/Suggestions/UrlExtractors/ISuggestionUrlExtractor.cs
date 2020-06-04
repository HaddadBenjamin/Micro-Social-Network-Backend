using System;

namespace DiabloII.Application.Hals.Domains.Suggestions.Decorators
{
    public interface ISuggestionUrlExtractor
    {
        string Create();

        string Delete(Guid suggestionId);

        string CreateComment(Guid suggestionId);

        string DeleteComment(Guid suggestionId, Guid commentId);

        string CreateVote(Guid suggestionId);
    }
}