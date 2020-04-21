using DiabloII.Infrastructure.DbContext;
using DiabloII.Infrastructure.Extensions;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Suggestions
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class SuggestionsTableCleanerSteps
    {
        private readonly ApplicationDbContext _dbContext;

        public SuggestionsTableCleanerSteps(ApplicationDbContext dbContext) => _dbContext = dbContext;

        [BeforeScenario]
        public void EmptyTheSuggestionTables()
        {
            _dbContext.EmptyTheTable(_dbContext.SuggestionVotes);
            _dbContext.EmptyTheTable(_dbContext.SuggestionComments);
            _dbContext.EmptyTheTable(_dbContext.Suggestions);

            _dbContext.SaveChanges();
        }
    }
}