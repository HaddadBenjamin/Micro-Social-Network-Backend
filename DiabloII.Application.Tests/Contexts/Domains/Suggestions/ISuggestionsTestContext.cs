using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Tests.Contexts.Bases;

namespace DiabloII.Application.Tests.Contexts.Domains.Suggestions
{
    public interface ISuggestionsTestContext :
        ITestContextAll<SuggestionDto>,
        ITestContextCreated<SuggestionDto>
    {
        SuggestionDto VotedResource { get; set; }

        SuggestionDto CommentedResource { get; set; }
    }
}