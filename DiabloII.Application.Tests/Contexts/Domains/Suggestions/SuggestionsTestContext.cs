using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Suggestions;

namespace DiabloII.Application.Tests.Contexts.Domains.Suggestions
{
    public class SuggestionsTestContext : ISuggestionsTestContext
    {
        public ApiResponses<SuggestionDto> Resources { get; set; }

        public SuggestionDto CreatedResource { get; set; }

        public SuggestionDto VotedResource { get; set; }

        public SuggestionDto CommentedResource { get; set; }

        public SuggestionDto GetResource { get; set; }
    }
}