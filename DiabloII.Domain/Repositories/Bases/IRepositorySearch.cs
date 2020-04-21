using System.Collections.Generic;

namespace DiabloII.Domain.Repositories.Bases
{
    public interface IRepositorySearch<Query, DataModel>
    {
        IReadOnlyCollection<DataModel> Search(Query query);
    }
}