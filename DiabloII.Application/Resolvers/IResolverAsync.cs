using System.Threading.Tasks;

namespace DiabloII.Application.Resolvers
{
    public interface IResolverAsync<ResolveResult>
    {
        Task<ResolveResult> ResolveAsync();
    }
}