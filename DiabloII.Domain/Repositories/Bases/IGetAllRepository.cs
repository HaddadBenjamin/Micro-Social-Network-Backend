using System.Collections.Generic;

namespace DiabloII.Domain.Repositories.Bases
{
    public interface IGetAllRepository<DomainType>
    {
        IReadOnlyCollection<DomainType> GetAll();
    }
}