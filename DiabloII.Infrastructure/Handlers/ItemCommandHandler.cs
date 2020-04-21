using System.Collections.Generic;
using DiabloII.Domain.Handlers;
using DiabloII.Domain.Models.Items;
using DiabloII.Domain.Repositories;
using DiabloII.Infrastructure.DbContext;

namespace DiabloII.Infrastructure.Handlers
{
    public class ItemCommandHandler : IItemCommandHandler
    {
        private readonly IItemRepository _repository;

        private readonly ApplicationDbContext _dbContext;

        public ItemCommandHandler(IItemRepository repository, ApplicationDbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public void Reset(IList<Item> items, IList<ItemProperty> itemProperties)
        {
            _repository.ResetTheItems(items, itemProperties);

            _dbContext.SaveChanges();
        }
    }
}