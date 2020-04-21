using System.Collections.Generic;

namespace DiabloII.Domain.Repositories.Bases
{
    public interface IRepositorySearch<Query, DomainType>
    {
        IReadOnlyCollection<DomainType> Search(Query query);
    }
}