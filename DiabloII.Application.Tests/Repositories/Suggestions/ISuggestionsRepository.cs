using System;
using System.Threading.Tasks;
using DiabloII.Application.Responses.Read.Suggestions;

namespace DiabloII.Application.Tests.Repositories.Suggestions
{
    public interface ISuggestionsRepository
    {
        Task<Guid> GetIdByItsContent(string content);

        Task<SuggestionDto> GetByItsContent(string content);

        SuggestionCommentDto GetCommentByItsContent(SuggestionDto suggestion, string commentContent);

        Guid GetCommentIdByItsContent(SuggestionDto suggestion, string commentContent);

        SuggestionVoteDto GetVoteCreatedBy(SuggestionDto suggestion, string createdBy);
    }
}