using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Responses.Suggestions;

namespace DiabloII.Application.Tests.Apis.Suggestions
{
    public interface ISuggestionsApi
    {
        #region  Read
        Task<IReadOnlyCollection<SuggestionDto>> GetAll();
        #endregion

        #region Write
        Task<SuggestionDto> Create(CreateASuggestionDto dto);

        Task<SuggestionDto> Vote(VoteToASuggestionDto dto);

        Task<SuggestionDto> Comment(CommentASuggestionDto dto);

        Task Delete(DeleteASuggestionDto dto);

        Task DeleteComment(DeleteASuggestionCommentDto dto);
        #endregion
    }
}