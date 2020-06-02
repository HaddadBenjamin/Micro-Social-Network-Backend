using System;
using System.Linq;
using System.Threading.Tasks;
using DiabloII.Application.Responses.Read.Suggestions;
using DiabloII.Application.Tests.Apis.Domains.Suggestions;

namespace DiabloII.Application.Tests.Repositories.Suggestions
{
    public class SuggestionsRepository : ISuggestionsRepository
    {
        private readonly ISuggestionsApi _suggestionsApi;

        public SuggestionsRepository(ISuggestionsApi suggestionsApi) => _suggestionsApi = suggestionsApi;

        #region Read
        public async Task<Guid> GetIdByItsContent(string content) => (await GetByItsContent(content)).Id;

        public async Task<SuggestionDto> GetByItsContent(string content) => (await _suggestionsApi.GetAll()).Elements
            .Single(suggestion => suggestion.Content == content);

        public SuggestionCommentDto GetCommentByItsContent(SuggestionDto suggestion, string commentContent) => suggestion.Comments
            .Single(comment => comment.Comment == commentContent);

        public Guid GetCommentIdByItsContent(SuggestionDto suggestion, string commentContent) =>
            GetCommentByItsContent(suggestion, commentContent).Id;

        public SuggestionVoteDto GetVoteCreatedBy(SuggestionDto suggestion, string createdBy) => suggestion.Votes
            .Single(vote => vote.CreatedBy == createdBy);
        #endregion
    }
}