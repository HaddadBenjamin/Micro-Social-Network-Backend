namespace DiabloII.Application.Resolvers
{
    public interface IResolver<ResolveResult>
    {
        ResolveResult Resolve();
    }
}
