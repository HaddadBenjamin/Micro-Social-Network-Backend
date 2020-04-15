using System.Threading.Tasks;
using DiabloII.Application.Tests.Apis.ErrorLogs;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.ErrorLogs.GetAll
{
    [Binding]
    [Scope(Tag = "errorlogs")]
    public class GetAllErrorLogsSteps
    {
        private readonly IErrorLogsApi _errorLogsApi;

        public GetAllErrorLogsSteps(IErrorLogsApi errorLogsApi) => _errorLogsApi = errorLogsApi;

        [When(@"I get all the error logs")]
        public async Task WhenIGetAllTheErrorLogs() => await _errorLogsApi.GetAll();
    }
}
