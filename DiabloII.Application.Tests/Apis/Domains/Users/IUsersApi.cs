using DiabloII.Application.Requests.Users;
using DiabloII.Application.Responses.Users;
using DiabloII.Application.Tests.Apis.Bases;

namespace DiabloII.Application.Tests.Apis.Domains.Users
{
    public interface IUsersApi :
        IGetAllApi<UserDto>,
        ICreateApi<CreateAUserDto, UserDto>,
        IUpdateApi<UpdateAUserDto, UserDto>
    {
    }
}