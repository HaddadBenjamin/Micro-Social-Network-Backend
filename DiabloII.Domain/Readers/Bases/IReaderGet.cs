namespace DiabloII.Domain.Readers.Bases
{
    public interface IReaderGet<DataModel, Query>
    {
        DataModel Get(Query query);
    }
}