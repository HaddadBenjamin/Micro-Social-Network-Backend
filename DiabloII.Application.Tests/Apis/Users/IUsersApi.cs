using System.Threading.Tasks;
using DiabloII.Application.Requests.Users;
using DiabloII.Application.Responses.Users;

namespace DiabloII.Application.Tests.Apis.Users
{
    public interface IUsersApi
    {
        Task<UserDto> Create(CreateAUserDto dto);
    }
}