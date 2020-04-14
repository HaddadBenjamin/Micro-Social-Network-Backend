using DiabloII.Application.Tests.Contexts;
using DiabloII.Infrastructure.DbContext;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Suggestions
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class SuggestionsTableCleanerSteps
    {
        private readonly ApplicationDbContext _dbContext;

        public SuggestionsTableCleanerSteps(TestContext testContext) => _dbContext = testContext.DbContext;

        [BeforeScenario]
        public void EmptyTheSuggestionTables()
        {
            _dbContext.SuggestionVotes.RemoveRange(_dbContext.SuggestionVotes);
            _dbContext.SuggestionComments.RemoveRange(_dbContext.SuggestionComments);
            _dbContext.Suggestions.RemoveRange(_dbContext.Suggestions);

            _dbContext.SaveChanges();
        }
    }
}