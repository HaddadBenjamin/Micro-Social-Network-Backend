using System;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Tests.Apis.Bases;

namespace DiabloII.Application.Tests.Apis.Domains.Suggestions
{
    public interface ISuggestionsApi :
        IGetAllApi<SuggestionDto>,
        ICreateApi<CreateASuggestionDto, SuggestionDto>,
        ICreateApi<VoteToASuggestionDto, SuggestionDto>,
        ICreateApi<CommentASuggestionDto, SuggestionDto>,
        IDeleteApi<DeleteASuggestionDto, Guid>,
        IDeleteApi<DeleteASuggestionCommentDto, SuggestionDto>
    {
    }
}