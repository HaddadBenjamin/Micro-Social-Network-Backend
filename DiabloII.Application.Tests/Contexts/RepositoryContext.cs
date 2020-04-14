namespace DiabloII.Application.Tests.Startup
{
    public class RepositoryContext
    {
        public SuggestionsRepository Suggestions { get; set; }

        public RepositoryContext(ApiContext apiContext) => Suggestions = new SuggestionsRepository(apiContext.Suggestions);
    }
}