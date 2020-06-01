using System;
using DiabloII.Application.Requests.Write.Suggestions;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Tests.Apis.Bases;
using DiabloII.Application.Tests.Models.Hals.Domains.Suggestions;

namespace DiabloII.Application.Tests.Apis.Domains.Suggestions
{
    public interface ISuggestionsApi :
        IApiGetAll<SuggestionDto>,
        IApiGet<SuggestionDto>,
        IApiCreate<CreateASuggestionDto, SuggestionDto>,
        IApiCreate<VoteToASuggestionDto, SuggestionDto>,
        IApiCreate<CommentASuggestionDto, SuggestionDto>,
        IApiDelete<DeleteASuggestionDto, Guid>,
        IApiDelete<DeleteASuggestionCommentDto, SuggestionDto>,
        IApiGetAllWithHals<HalSuggestionsDto>
    {
    }
}