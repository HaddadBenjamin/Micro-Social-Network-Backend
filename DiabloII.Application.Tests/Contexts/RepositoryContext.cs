using DiabloII.Application.Tests.Steps.Suggestions;

namespace DiabloII.Application.Tests.Contexts
{
    public class RepositoryContext
    {
        public SuggestionsRepository Suggestions { get; set; }

        public RepositoryContext(ApiContext apiContext) => Suggestions = new SuggestionsRepository(apiContext.Suggestions);
    }
}