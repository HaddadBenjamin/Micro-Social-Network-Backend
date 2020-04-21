using System.Linq;
using System.Threading.Tasks;
using DiabloII.Application.Tests.Apis.Domains.Users;
using DiabloII.Application.Tests.Contexts.Domains.Users;
using DiabloII.Application.Tests.Extensions;
using DiabloII.Application.Tests.Mappers;
using DiabloII.Domain.Repositories;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps.Users.Update
{
    [Binding]
    [Scope(Tag = "users")]
    public class UpdateAUserSteps
    {
        private readonly IUsers _users;
      
        private readonly IUsersTestContext _userContext;
        
        private readonly IUserRepository _repository;

        public UpdateAUserSteps(IUsers users, IUsersTestContext userContext, IUserRepository repository)
        {
            _users = users;
            _userContext = userContext;
            _repository = repository;
        }

        [When(@"I update the user ""(.*)"" with the following informations")]
        public async Task WhenIUpdateTheUserWithTheFollowingInformations(string email, Table table)
        {
            var userId = _repository.GetUserIdByItsEmail(email);
            var dto = UsersTableMapper.ToUpdateAUserDto(table.Rows.First(), userId);

            _userContext.UpdatedResource = await _users.Update(dto);
        }

        [Then(@"the updated user should be")]
        public void ThenTheUpdatedUserShouldBe(Table table) =>
            table.ShouldBeEqualsTo(_userContext.UpdatedResource, UsersTableMapper.ToUserDto);
    }
}