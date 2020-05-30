using DiabloII.Application.Responses;
using DiabloII.Application.Responses.Suggestions;

namespace DiabloII.Application.Tests.Contexts.Domains.Suggestions
{
    public class SuggestionsTestContext : ISuggestionsTestContext
    {
        public ApiResponses<SuggestionDto> Resources { get; set; }

        public SuggestionDto CreatedResource { get; set; }

        public SuggestionDto VotedResource { get; set; }

        public SuggestionDto CommentedResource { get; set; }
    }
}