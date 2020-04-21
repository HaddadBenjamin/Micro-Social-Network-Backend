using System.Threading.Tasks;
using DiabloII.Application.Requests.Users;
using DiabloII.Application.Tests.Apis.Domains.Users;
using DiabloII.Application.Tests.Contexts.Users;
using DiabloII.Application.Tests.Extensions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Users.Create
{
    [Binding]
    [Scope(Tag = "users")]
    public class CreateAUserSteps
    {
        private readonly IUsersApi _usersApi;
      
        private readonly IUsersTestContext _userContext;

        public CreateAUserSteps(IUsersApi usersApi, IUsersTestContext userContext)
        {
            _usersApi = usersApi;
            _userContext = userContext;
        }

        [Given(@"I create the users with the following informations")]
        [When(@"I create the users with the following informations")]
        public async Task WhenICreateTheUsersWithTheFollowingInformations(Table table)
        {
            var dtos = table.CreateSet<CreateAUserDto>();

            foreach (var dto in dtos)
                _userContext.CreatedUser = await _usersApi.Create(dto);
        }

        [Then(@"the created user should be")]
        public void ThenTheCreatedUserShouldBe(Table table) =>  table.ShouldBeEqualsTo(_userContext.CreatedUser);
    }
}