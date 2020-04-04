using System.Collections.Generic;
using DiabloII.Items.Api.Domain.Models.Items;
using DiabloII.Items.Api.Domain.Queries.Items;
using DiabloII.Items.Api.Infrastructure.DbContext;
using DiabloII.Items.Api.Infrastructure.Repositories.Items;

namespace DiabloII.Items.Api.Application.Services.Items
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