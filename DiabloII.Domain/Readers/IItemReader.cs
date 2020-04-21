using DiabloII.Domain.Models.Items;
using DiabloII.Domain.Queries.Items;
using DiabloII.Domain.Readers.Bases;

namespace DiabloII.Domain.Readers
{
    public interface IItemReader :
        IGetAllReader<Item>,
        ISearchReader<SearchUniquesQuery, Item>
    {
    }
}