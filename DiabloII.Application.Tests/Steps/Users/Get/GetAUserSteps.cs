using System.Threading.Tasks;
using DiabloII.Application.Tests.Apis.Domains.Users;
using DiabloII.Application.Tests.Contexts.Domains.Users;
using DiabloII.Application.Tests.Extensions;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Users.Get
{
    [Binding]
    [Scope(Tag = "users")]
    public class GetAUserSteps
    {
        private readonly IUsersApi _usersApi;

        private readonly IUsersTestContext _usersContext;

        public GetAUserSteps(IUsersApi usersApi, IUsersTestContext usersContext)
        {
            _usersApi = usersApi;
            _usersContext = usersContext;
        }

        [When(@"I get the created user")]
        public async Task WhenIGetTheCreatedUser()
        {
            var createdUserId = _usersContext.CreatedResourceId;

            _usersContext.GetResource = await _usersApi.Get(createdUserId);
        }

        [When(@"I get the updated user")]
        public async Task WhenIGetTheUpdatedUser()
        {
            var updatedUserId = _usersContext.UpdatedResourceId;

            _usersContext.GetResource = await _usersApi.Get(updatedUserId);
        }


        [Then(@"the user should be")]
        public void ThenTheUserShouldBe(Table table) =>
            table.ShouldBeEqualsTo(_usersContext.GetResource);
    }
}