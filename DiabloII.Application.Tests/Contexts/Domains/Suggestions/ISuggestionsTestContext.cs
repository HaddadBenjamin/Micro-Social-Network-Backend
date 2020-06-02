using System;
using DiabloII.Application.Responses.Read.Suggestions;
using DiabloII.Application.Tests.Contexts.Bases;

namespace DiabloII.Application.Tests.Contexts.Domains.Suggestions
{
    public interface ISuggestionsTestContext :
        ITestContextAll<SuggestionDto>,
        ITestContextGet<SuggestionDto>,
        ITestContextCreated
    {
        Guid VotedResourceId { get; set; }

        Guid CommentedResourceId { get; set; }
    }
}