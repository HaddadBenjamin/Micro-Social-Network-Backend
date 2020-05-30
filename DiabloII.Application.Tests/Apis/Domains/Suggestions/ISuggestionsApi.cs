using System;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Tests.Apis.Bases;

namespace DiabloII.Application.Tests.Apis.Domains.Suggestions
{
    public interface ISuggestionsApi :
        IApiGetResponses<SuggestionDto>,
        IApiCreate<CreateASuggestionDto, SuggestionDto>,
        IApiCreate<VoteToASuggestionDto, SuggestionDto>,
        IApiCreate<CommentASuggestionDto, SuggestionDto>,
        IApiDelete<DeleteASuggestionDto, Guid>,
        IApiDelete<DeleteASuggestionCommentDto, SuggestionDto>
    {
    }
}