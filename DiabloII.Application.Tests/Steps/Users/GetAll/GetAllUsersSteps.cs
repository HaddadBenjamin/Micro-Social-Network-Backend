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
        private readonly IUsers _users;
       
        private readonly IUsersTestContext _usersContext;

        public GetAllUsersSteps(IUsers users, IUsersTestContext usersContext)
        {
            _users = users;
            _usersContext = usersContext;
        }

        [When(@"I get all the users")]
        public async Task WhenIGetAllTheUsers() => _usersContext.AllResources = await _users.GetAll();

        [Then(@"all the users should be")]
        public void ThenAllTheUsersShouldBe(Table table) => table.ShouldAllExistsIn(_usersContext.AllResources);
    }
}
