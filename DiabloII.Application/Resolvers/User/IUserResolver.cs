using UserModel = DiabloII.Domain.Models.Users.User;

namespace DiabloII.Application.Resolvers.User
{
    public interface IUserResolver : IResolverAsync<UserModel>
    {
    }
}
