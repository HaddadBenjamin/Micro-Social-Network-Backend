using DiabloII.Application.Resolvers.Bases;
using UserModel = DiabloII.Domain.Models.Users.User;

namespace DiabloII.Application.Resolvers.Implementations.User
{
    public interface IUserResolver : IResolverAsync<UserModel>
    {
    }
}
