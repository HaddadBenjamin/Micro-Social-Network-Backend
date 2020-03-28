using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Items.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DiabloII.Items.Api.Helpers;
using DiabloII.Items.Api.Queries;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace DiabloII.Items.Api.Services.Items
{
    public class ItemsService : IItemsService
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemsService(ApplicationDbContext dbContext) => _dbContext = dbContext;

        private IQueryable<Item> UniqueItems => _dbContext.Items
            .Include(unique => unique.Properties)
            .Where(unique => unique.Quality == ItemQuality.Unique); 
        
        public void ResetTheItems(IList<Item> items, IList<ItemProperty> itemProperties)
        {
            _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE ItemProperties");
            _dbContext.Database.ExecuteSqlCommand("DELETE FROM Items");
           
            _dbContext.BulkInsert(items);
            _dbContext.BulkInsert(itemProperties);

            _dbContext.SaveChanges();
        }

        public IReadOnlyCollection<Item> GetAllUniques() => UniqueItems.ToList();

        public IReadOnlyCollection<Item> SearchUniques(SearchUniquesQuery query) =>UniqueItems
            .Where(unique =>
                (query.MinimumLevel == null || unique.Level >= query.MinimumLevel) &&
                (query.MaximumLevel == null || unique.Level >= query.MaximumLevel) &&
                (EnumerableHelpers.IsNullOrEmpty(query.SubCategories) ||
                 query.SubCategories.Contains(unique.SubCategory)))
            .ToList();
    }

    public static class IQueryableExtensions
    {
        private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

        private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryCompiler");

        private static readonly FieldInfo QueryModelGeneratorField = QueryCompilerTypeInfo.DeclaredFields.First(x => x.Name == "_queryModelGenerator");

        private static readonly FieldInfo DataBaseField = QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

        private static readonly PropertyInfo DatabaseDependenciesField = typeof(Database).GetTypeInfo().DeclaredProperties.Single(x => x.Name == "Dependencies");

        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            var queryCompiler = (QueryCompiler)QueryCompilerField.GetValue(query.Provider);
            var modelGenerator = (QueryModelGenerator)QueryModelGeneratorField.GetValue(queryCompiler);
            var queryModel = modelGenerator.ParseQuery(query.Expression);
            var database = (IDatabase)DataBaseField.GetValue(queryCompiler);
            var databaseDependencies = (DatabaseDependencies)DatabaseDependenciesField.GetValue(database);
            var queryCompilationContext = databaseDependencies.QueryCompilationContextFactory.Create(false);
            var modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();
            modelVisitor.CreateQueryExecutor<TEntity>(queryModel);
            var sql = modelVisitor.Queries.First().ToString();

            return sql;
        }
    }
}