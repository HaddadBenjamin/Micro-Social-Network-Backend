using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Items.Models;
using System.Collections.Generic;
using DiabloII.Items.Api.Queries;
using DiabloII.Items.Api.Repositories.Items;

namespace DiabloII.Items.Api.Services.Items
{
    public class ItemsService : IItemsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IItemRepository _repository;

        public ItemsService(ApplicationDbContext dbContext, IItemRepository repository)
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