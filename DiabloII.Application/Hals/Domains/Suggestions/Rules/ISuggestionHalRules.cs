using DiabloII.Application.Responses.Read.Suggestions;

namespace DiabloII.Application.Hals.Domains.Suggestions.Rules
{
    public interface ISuggestionHalRules
    {
        bool CanEditASuggestion(SuggestionDto suggestion);

        bool CanAddAVote(SuggestionDto suggestion);

        bool CanEditAComment(SuggestionCommentDto comment);
    }
}