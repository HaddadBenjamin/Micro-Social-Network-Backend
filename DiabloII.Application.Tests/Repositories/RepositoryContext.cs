using DiabloII.Application.Tests.Apis;

namespace DiabloII.Application.Tests.Repositories
{
    public class RepositoryContext
    {
        public readonly SuggestionsRepository Suggestions;

        public RepositoryContext(ApiContext apiContext) => Suggestions = new SuggestionsRepository(apiContext.Suggestions);
    }
}