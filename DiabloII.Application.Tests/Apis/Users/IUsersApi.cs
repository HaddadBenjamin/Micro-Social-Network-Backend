using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Notifications;
using DiabloII.Application.Requests.Users;
using DiabloII.Application.Responses.Notifications;
using DiabloII.Application.Responses.Users;

namespace DiabloII.Application.Tests.Apis.Users
{
    public interface IUsersApi
    {
        #region Read
        Task<IReadOnlyCollection<UserDto>> GetAll();
        #endregion
        
        #region Write
        Task<UserDto> Create(CreateAUserDto dto);

        Task<UserDto> Update(UpdateAUserDto dto);
        #endregion
    }
}