using System;
using DiabloII.Application.Requests.Write.Suggestions;
using DiabloII.Application.Responses.Read.Suggestions;
using DiabloII.Application.Tests.Apis.Bases;
using DiabloII.Application.Tests.Models.Hals.Domains.Suggestions;

namespace DiabloII.Application.Tests.Apis.Domains.Suggestions
{
    public interface ISuggestionsApi :
        IApiGetAll<SuggestionDto>,
        IApiGet<SuggestionDto>,
        IApiCreate<CreateASuggestionDto>,
        IApiCreate<VoteToASuggestionDto>,
        IApiCreate<CommentASuggestionDto>,
        IApiDelete<DeleteASuggestionDto>,
        IApiDelete<DeleteASuggestionCommentDto>,
        IApiGetAllWithHals<HalSuggestionsDto>
    {
    }
}