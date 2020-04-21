using System;
using System.Linq;
using System.Threading.Tasks;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Tests.Apis.Domains.Suggestions;

namespace DiabloII.Application.Tests.Repositories.Suggestions
{
    public class SuggestionsRepository : ISuggestionsRepository
    {
        private readonly ISuggestions _suggestions;

        public SuggestionsRepository(ISuggestions suggestions) => _suggestions = suggestions;

        #region Read
        public async Task<Guid> GetSuggestionId(string suggestionContent) => (await GetSuggestion(suggestionContent)).Id;

        public async Task<SuggestionDto> GetSuggestion(string suggestionContent) => (await _suggestions.GetAll())
            .Single(suggestion => suggestion.Content == suggestionContent);

        public Guid GetSuggestionCommentId(SuggestionDto suggestionDto, string suggestionCommentContent) => suggestionDto.Comments
            .Single(comment => comment.Comment == suggestionCommentContent).Id;
        #endregion
    }
}