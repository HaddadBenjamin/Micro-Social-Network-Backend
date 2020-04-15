using System;
using System.Threading.Tasks;
using DiabloII.Application.Responses.Suggestions;

namespace DiabloII.Application.Tests.Repositories
{
    public interface ISuggestionsRepository
    {
        Task<Guid> GetSuggestionId(string suggestionContent);

        Task<SuggestionDto> GetSuggestion(string suggestionContent);

        Guid GetSuggestionCommentId(SuggestionDto suggestionDto, string suggestionCommentContent);
    }
}