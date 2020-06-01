using System.Threading.Tasks;

namespace DiabloII.Application.Resolvers.Bases
{
    public interface IResolverAsync<ResolveResult>
    {
        Task<ResolveResult> ResolveAsync();
    }
}