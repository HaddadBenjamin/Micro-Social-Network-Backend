using System.Collections.Generic;

namespace DiabloII.Domain.Readers.Bases
{
    public interface IGetAllReader<DataModel>
    {
        IReadOnlyCollection<DataModel> GetAll();
    }
}