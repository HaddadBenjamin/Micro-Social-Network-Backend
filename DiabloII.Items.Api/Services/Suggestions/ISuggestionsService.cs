using System.Collections.Generic;
using DiabloII.Items.Api.Queries.Suggestions;
using DiabloII.Items.Api.Responses.Suggestions;

namespace DiabloII.Items.Api.Services.Suggestions
{
    public interface ISuggestionsService
    {
        void Create(CreateASuggestionDto createASugestion);

        SuggestionDto Vote(VoteToASuggestionDto voteToASuggestion);

        IReadOnlyCollection<SuggestionDto> GetAll();
    }
}