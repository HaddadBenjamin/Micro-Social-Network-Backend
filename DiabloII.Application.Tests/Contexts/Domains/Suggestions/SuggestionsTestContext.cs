using System;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Suggestions;

namespace DiabloII.Application.Tests.Contexts.Domains.Suggestions
{
    public class SuggestionsTestContext : ISuggestionsTestContext
    {
        public ApiResponses<SuggestionDto> Resources { get; set; }

        public Guid CreatedResourceId { get; set; }

        public Guid VotedResourceId { get; set; }

        public Guid CommentedResourceId { get; set; }

        public SuggestionDto GetResource { get; set; }
    }
}