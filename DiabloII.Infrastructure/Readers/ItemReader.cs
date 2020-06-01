using System.Collections.Generic;
using DiabloII.Domain.Models.Items;
using DiabloII.Domain.Queries.Domains.Items;
using DiabloII.Domain.Readers;
using DiabloII.Domain.Repositories;

namespace DiabloII.Infrastructure.Readers
{
    public class ItemReader : IItemReader
    {
        private readonly IItemRepository _repository;

        public ItemReader(IItemRepository repository) => _repository = repository;

        #region Read
        public IReadOnlyCollection<Item> GetAll() => _repository.GetAll();

        public IReadOnlyCollection<Item> Search(SearchUniquesQuery query) => _repository.Search(query);
        #endregion
    }
}