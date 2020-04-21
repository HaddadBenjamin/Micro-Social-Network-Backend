using System.Collections.Generic;

namespace DiabloII.Domain.Readers.Bases
{
    public interface IReaderGetAll<DataModel>
    {
        IReadOnlyCollection<DataModel> GetAll();
    }
}