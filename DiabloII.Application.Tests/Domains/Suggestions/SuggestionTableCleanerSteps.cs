using DiabloII.Application.Tests.Startup;
using DiabloII.Infrastructure.DbContext;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Domains.Suggestions
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class SuggestionTableCleanerSteps
    {
        private readonly ApplicationDbContext _dbContext;

        public SuggestionTableCleanerSteps(MyTestContext testContext)
        {
            _dbContext = testContext.DbContext;
        }

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