using System.Collections.Generic;

namespace DiabloII.Domain.Readers.Bases
{
    public interface ISearchReader<Query, DataModel>
    {
        IReadOnlyCollection<DataModel> Search(Query query);
    }
}