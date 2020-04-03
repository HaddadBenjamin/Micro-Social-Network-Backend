using System;
using System.Collections.Generic;
using DiabloII.Items.Api.Requests.Suggestions;
using DiabloII.Items.Api.Responses.Suggestions;

namespace DiabloII.Items.Api.Services.Suggestions
{
    public interface ISuggestionsService
    {
        SuggestionDto Create(CreateASuggestionDto createASugestion);

        SuggestionDto Vote(VoteToASuggestionDto voteToASuggestion);

        IReadOnlyCollection<SuggestionDto> GetAll();

        Guid Delete(DeleteASuggestionDto deleteASuggestion);
    }
}