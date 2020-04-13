using DiabloII.Application.Tests.Startup;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Suggestions
{
    [Binding]
    [Scope(Tag = "suggestion")]
    public class SuggestionTableCleanerSteps
    {
        [SetUp]
        public void EmptyTheSuggestionTables()
        {
            var dbContext = MyTestContext.Instance.DbContext;

            dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE SuggestionVotes");
            dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE SuggestionComments");
            dbContext.Database.ExecuteSqlCommand("DELETE FROM Suggestions");
        }
    }
}