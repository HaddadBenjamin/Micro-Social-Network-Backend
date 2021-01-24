using System;

namespace DiabloII.Application.Hals.Domains.Suggestions.Decorators
{
    public class SuggestionUrlExtractor : ISuggestionUrlExtractor
    {
        private static readonly string _domain = "suggestions";

        public string Create() => _domain;

        public string Delete(Guid suggestionId) => $"{_domain}/{suggestionId}";

        public string CreateComment(Guid suggestionId) => $"{_domain}/{suggestionId}/comments";

        public string DeleteComment(Guid suggestionId, Guid commentId) => $"{_domain}/{suggestionId}/comments/{commentId}";

        public string CreateVote(Guid suggestionId) => $"{_domain}/{suggestionId}/votes"; 
    }
}