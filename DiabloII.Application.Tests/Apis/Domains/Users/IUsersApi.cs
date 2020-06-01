using System.Threading.Tasks;
using DiabloII.Application.Requests.Write.Users;
using DiabloII.Application.Responses.Users;
using DiabloII.Application.Tests.Apis.Bases;

namespace DiabloII.Application.Tests.Apis.Domains.Users
{
    public interface IUsersApi :
        IApiGetAll<UserDto>,
        IApiCreate<CreateAUserDto, UserDto>,
        IUpdateApi<UpdateAUserDto, UserDto>
    {
        Task<UserDto> IdentifyMe();
    }
}