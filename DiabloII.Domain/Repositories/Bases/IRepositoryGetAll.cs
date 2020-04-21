using System.Collections.Generic;

namespace DiabloII.Domain.Repositories.Bases
{
    public interface IRepositoryGetAll<DataModel>
    {
        IReadOnlyCollection<DataModel> GetAll();
    }
}