namespace DiabloII.Domain.Repositories.Bases
{
    public interface IRepositoryGet<DataModel, Id>
    {
        DataModel Get(Id id);
    }
}