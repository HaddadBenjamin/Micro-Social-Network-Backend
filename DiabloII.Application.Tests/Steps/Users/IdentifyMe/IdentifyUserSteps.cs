using System.Threading.Tasks;
using DiabloII.Application.Tests.Apis.Domains.Users;
using DiabloII.Application.Tests.Contexts.Domains.Users;
using Shouldly;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Users.IdentifyMe
{
    [Binding]
    [Scope(Tag = "users")]
    public class IdentifyUserSteps
    {
        private readonly IUsersApi _usersApi;

        private readonly IUsersTestContext _usersContext;

        public IdentifyUserSteps(IUsersApi usersApi, IUsersTestContext usersContext)
        {
            _usersApi = usersApi;
            _usersContext = usersContext;
        }

        [Given(@"I identify me")]
        public async Task GivenIIdentifyMe() =>
            _usersContext.IdentifiedUser = await _usersApi.IdentifyMe();

        [Then(@"the identified user should exists")]
        public void ThenTheIdentifiedUserShouldExists() =>
            _usersContext.IdentifiedUser.ShouldNotBeNull();
    }
}
