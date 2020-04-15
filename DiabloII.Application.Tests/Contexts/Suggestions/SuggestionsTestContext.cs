using System.Collections.Generic;
using DiabloII.Application.Responses.Suggestions;

namespace DiabloII.Application.Tests.Contexts.Suggestions
{
    public class SuggestionsTestContext : ISuggestionsTestContext
    {
        public IReadOnlyCollection<SuggestionDto> AllSuggestions { get; set; }

        public SuggestionDto CreatedSuggestion { get; set; }

        public SuggestionDto VotedSuggestion { get; set; }

        public SuggestionDto CommentedSuggestion { get; set; }
    }
}