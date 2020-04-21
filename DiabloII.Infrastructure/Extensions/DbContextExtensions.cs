using Microsoft.EntityFrameworkCore;

namespace DiabloII.Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        public static void EmptyTheTable<DbSetType>(this Microsoft.EntityFrameworkCore.DbContext dbContext, DbSet<DbSetType> dbSet)
            where DbSetType : class =>
            dbSet.RemoveRange(dbSet);
    }
}