namespace DiabloII.Domain.Repositories.Bases
{
    public interface IGetRepository<DomainType, Id>
    {
        DomainType Get(Id id);
    }
}