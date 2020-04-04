using System.Collections.Generic;
using System.Linq;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Items.Models;
using DiabloII.Items.Api.Helpers;
using DiabloII.Items.Api.Queries.Items;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Items.Api.Repositories.Items
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public void ResetTheItems(IList<Item> items, IList<ItemProperty> itemProperties)
        {
            _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE ItemProperties");
            _dbContext.Database.ExecuteSqlCommand("DELETE FROM Items");

            _dbContext.BulkInsert(items);
            _dbContext.BulkInsert(itemProperties);
        }

        public IReadOnlyCollection<Item> GetAllUniques() => UniqueItems.ToList();

        public IReadOnlyCollection<Item> SearchUniques(SearchUniquesQuery query) => UniqueItems
            .Where(unique =>
                (query.MinimumLevel == null || unique.Level >= query.MinimumLevel) &&
                (query.MaximumLevel == null || unique.Level >= query.MaximumLevel) &&
                (EnumerableHelpers.IsNullOrEmpty(query.SubCategories) || query.SubCategories.Contains(unique.SubCategory)))
            .ToList();

        private IQueryable<Item> UniqueItems => _dbContext.Items
            .Include(unique => unique.Properties)
            .Where(unique => unique.Quality == ItemQuality.Unique);
    }
}