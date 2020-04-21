using DiabloII.Application.Requests.Users;
using DiabloII.Application.Responses.Users;
using DiabloII.Application.Tests.Apis.Bases;

namespace DiabloII.Application.Tests.Apis.Domains.Users
{
    public interface IUsers :
        IApiGetAll<UserDto>,
        IApiCreate<CreateAUserDto, UserDto>,
        IApiUpdate<UpdateAUserDto, UserDto>
    {
    }
}