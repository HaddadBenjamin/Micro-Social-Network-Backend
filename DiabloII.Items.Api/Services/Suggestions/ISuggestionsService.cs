using System.Collections.Generic;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Queries.Suggestions;
using DiabloII.Items.Api.Responses.Suggestions;

namespace DiabloII.Items.Api.Services.Suggestions
{
    public interface ISuggestionsService
    {
        void Create(CreateSuggestionDto createSugestion, ApplicationDbContext applicationDbContext);

        SuggestionDto Vote(SuggestionVoteDto voteSuggestion, ApplicationDbContext applicationDbContext);

        IReadOnlyCollection<SuggestionDto> GetAll(ApplicationDbContext applicationDbContext);
    }
}