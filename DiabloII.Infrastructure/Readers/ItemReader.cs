using System.Collections.Generic;
using DiabloII.Domain.Models.Items;
using DiabloII.Domain.Queries.Items;
using DiabloII.Domain.Readers;
using DiabloII.Domain.Repositories;
using DiabloII.Infrastructure.DbContext;

namespace DiabloII.Infrastructure.Readers
{
    public class ItemReader : IItemReader
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IItemRepository _repository;

        public ItemReader(ApplicationDbContext dbContext, IItemRepository repository)
        {
            _dbContext = dbContext;
            _repository = repository;
        }

        #region Read
        public IReadOnlyCollection<Item> GetAllUniques() => _repository.GetAllUniques();

        public IReadOnlyCollection<Item> SearchUniques(SearchUniquesQuery query) => _repository.SearchUniques(query);
        #endregion

        #region Write
        public void ResetTheItems(IList<Item> items, IList<ItemProperty> itemProperties)
        {
            _repository.ResetTheItems(items, itemProperties);

            _dbContext.SaveChanges();
        }
        #endregion
    }
}