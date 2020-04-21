namespace DiabloII.Domain.Repositories.Bases
{
    public interface IRepositoryGet<DomainType, Id>
    {
        DomainType Get(Id id);
    }
}