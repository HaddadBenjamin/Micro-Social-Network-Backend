using System.Collections.Generic;

namespace DiabloII.Domain.Repositories.Bases
{
    public interface IRepositoryGetAll<DomainType>
    {
        IReadOnlyCollection<DomainType> GetAll();
    }
}