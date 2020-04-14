using System.Collections.Generic;
using DiabloII.Application.Responses.Suggestions;

namespace DiabloII.Application.Tests.Steps.Suggestions
{
    public class SuggestionTestContext
    {
        public IReadOnlyCollection<SuggestionDto> AllSuggestions { get; set; }

        public SuggestionDto CreatedSuggestion { get; set; }

        public SuggestionDto VotedSuggestion { get; set; }

        public SuggestionDto CommentedSuggestion { get; set; }
    }
}