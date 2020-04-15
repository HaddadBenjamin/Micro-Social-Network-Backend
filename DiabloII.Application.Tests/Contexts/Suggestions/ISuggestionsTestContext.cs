using System.Collections.Generic;
using DiabloII.Application.Responses.Suggestions;

namespace DiabloII.Application.Tests.Contexts.Suggestions
{
    public interface ISuggestionsTestContext
    {
        IReadOnlyCollection<SuggestionDto> AllSuggestions { get; set; }

        SuggestionDto CreatedSuggestion { get; set; }

        SuggestionDto VotedSuggestion { get; set; }

        SuggestionDto CommentedSuggestion { get; set; }
    }
}