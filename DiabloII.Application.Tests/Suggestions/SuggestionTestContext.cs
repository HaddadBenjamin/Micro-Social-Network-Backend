using System.Collections.Generic;
using DiabloII.Application.Responses.Suggestions;

namespace DiabloII.Application.Tests.Suggestions
{
    public class SuggestionTestContext
    {
        public SuggestionDto CreatedSuggestion { get; set; }

        public IReadOnlyCollection<SuggestionDto> AllSuggestions { get; set; }
    }
}