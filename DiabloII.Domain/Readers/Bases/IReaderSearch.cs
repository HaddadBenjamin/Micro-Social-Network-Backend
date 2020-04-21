using System.Collections.Generic;

namespace DiabloII.Domain.Readers.Bases
{
    public interface IReaderSearch<Query, DataModel>
    {
        IReadOnlyCollection<DataModel> Search(Query query);
    }
}