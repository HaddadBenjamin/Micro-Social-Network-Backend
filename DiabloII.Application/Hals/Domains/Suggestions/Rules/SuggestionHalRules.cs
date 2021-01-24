using DiabloII.Application.Resolvers.Implementations.UserId;
using DiabloII.Application.Responses.Read.Suggestions;

namespace DiabloII.Application.Hals.Domains.Suggestions.Rules
{
    public class SuggestionHalRules : ISuggestionHalRules
    {
        private readonly string _userId;

        public SuggestionHalRules(IUserIdResolver userIdResolver) =>
            _userId = userIdResolver.Resolve();

        public bool CanEditASuggestion(SuggestionDto suggestion) =>
            _userId == suggestion.CreatedBy;

        public bool CanAddAVote(SuggestionDto suggestion) =>
            _userId != suggestion.CreatedBy;

        public bool CanEditAComment(SuggestionCommentDto comment) =>
            _userId == comment.CreatedBy;
    }
}