using DiabloII.Domain.Models.Items;
using DiabloII.Domain.Queries.Domains.Items;
using DiabloII.Domain.Readers.Bases;

namespace DiabloII.Domain.Readers.Domains
{
    public interface IItemReader :
        IReaderGetAll<Item>,
        IReaderSearch<SearchUniquesQuery, Item>
    {
    }
}