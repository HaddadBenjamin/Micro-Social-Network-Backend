using System.Threading.Tasks;
using DiabloII.Application.Tests.Apis.Domains.Users;
using DiabloII.Application.Tests.Contexts.Domains.Users;
using DiabloII.Application.Tests.Extensions;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Users.GetAll
{
    [Binding]
    [Scope(Tag = "users")]
    public class GetAllUsersSteps
    {
        private readonly IUsersApi _usersApi;

        private readonly IUsersTestContext _usersContext;

        public GetAllUsersSteps(IUsersApi usersApi, IUsersTestContext usersContext)
        {
            _usersApi = usersApi;
            _usersContext = usersContext;
        }

        [When(@"I get all the users")]
        public async Task WhenIGetAllTheUsers() => _usersContext.AllResources = await _usersApi.GetAll();

        [Then(@"all the users should be")]
        public void ThenAllTheUsersShouldBe(Table table) => table.ShouldAllExistsIn(_usersContext.AllResources);
    }
}
