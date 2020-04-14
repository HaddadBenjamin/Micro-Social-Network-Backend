using System.Threading.Tasks;
using DiabloII.Application.Tests.Contexts;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.ErrorLogs.GetAll
{
    [Binding]
    [Scope(Tag = "errorlogs")]
    public class GetAllErrorLogsSteps
    {
        private readonly ErrorLogsApi _errorLogsApi;

        public GetAllErrorLogsSteps(TestContext testContext) => _errorLogsApi = testContext.ApiContext.ErrorLogs;

        [When(@"I get all the error logs")]
        public async Task WhenIGetAllTheErrorLogs() => await _errorLogsApi.GetAll();
    }
}
