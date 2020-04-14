using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Tests.Steps.Suggestions;

namespace DiabloII.Application.Tests.Startup
{
    public class SuggestionsRepository
    {
        private readonly SuggestionsApi _suggestionsApi;

        public SuggestionsRepository(SuggestionsApi suggestionsApi) => _suggestionsApi = suggestionsApi;

        #region Read
        public async Task<Guid> GetSuggestionId(string suggestionContent) => (await _suggestionsApi.GetAll())
            .Single(suggestion => suggestion.Content == suggestionContent)
            .Id;

        public async Task<SuggestionDto> GetSuggestion(string suggestionContent) => (await _suggestionsApi.GetAll())
            .Single(suggestion => suggestion.Content == suggestionContent);

        public Guid GetSuggestionCommentId(SuggestionDto suggestionDto, string suggestionCommentContent) => suggestionDto.Comments
            .Single(comment => comment.Comment == suggestionCommentContent).Id;
        #endregion
    }
}