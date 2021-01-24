using System.Threading.Tasks;
using DiabloII.Application.Requests.Write.Users;
using DiabloII.Application.Responses.Read.Users;
using DiabloII.Application.Tests.Apis.Bases;

namespace DiabloII.Application.Tests.Apis.Domains.Users
{
    public interface IUsersApi :
        IApiGetAll<UserDto>,
        IApiGet<UserDto, string>,
        IApiCreate<CreateAUserDto, string>,
        IUpdateApi<UpdateAUserDto, string>
    {
        Task<UserDto> IdentifyMe();
    }
}