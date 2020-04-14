namespace DiabloII.Application.Tests.Startup
{
    public class Repositories
    {
        public SuggestionsRepository Suggestions { get; set; }

        public Repositories(ApiContext apiContext) =>
            Suggestions = new SuggestionsRepository(apiContext.Suggestions);
    }
}