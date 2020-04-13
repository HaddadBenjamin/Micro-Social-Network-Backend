using System.Threading.Tasks;
using DiabloII.Application.Tests.Domains.Items;
using DiabloII.Application.Tests.Startup;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Domains.Suggestions.GetAll
{
    [Binding]
    [Scope(Tag = "errorlogs")]
    public class GetAllErrorLogsSteps
    {
        private readonly ErrorLogsApi _errorLogsApi;

        public GetAllErrorLogsSteps(MyTestContext testContext) => _errorLogsApi = testContext.Apis.ErrorLogs;

        [When(@"I get all the error logs")]
        public async Task WhenIGetAllTheErrorLogs() => await _errorLogsApi.GetAll();
    }
}
