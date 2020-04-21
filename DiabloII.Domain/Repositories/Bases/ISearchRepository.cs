using System.Collections.Generic;

namespace DiabloII.Domain.Repositories.Bases
{
    public interface ISearchRepository<Query, DomainType>
    {
        IReadOnlyCollection<DomainType> Search(Query query);
    }
}