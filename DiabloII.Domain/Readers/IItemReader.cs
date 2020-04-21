using DiabloII.Domain.Models.Items;
using DiabloII.Domain.Queries.Items;
using DiabloII.Domain.Readers.Bases;

namespace DiabloII.Domain.Readers
{
    public interface IItemReader :
        IReaderGetAll<Item>,
        IReaderSearch<SearchUniquesQuery, Item>
    {
    }
}