using System.Collections.Generic;
using DiabloII.Application.Responses.Suggestions;

namespace DiabloII.Application.Tests.Contexts.Domains.Suggestions
{
    public class SuggestionsTestContext : ISuggestionsTestContext
    {
        public IReadOnlyCollection<SuggestionDto> AllResources { get; set; }

        public SuggestionDto CreatedResource { get; set; }

        public SuggestionDto VotedResource { get; set; }

        public SuggestionDto CommentedResource { get; set; }
    }
}