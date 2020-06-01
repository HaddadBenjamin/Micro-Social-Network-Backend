using System.Collections.Generic;
using System.Linq;
using DiabloII.Domain.Models.ErrorLogs;
using DiabloII.Domain.Repositories.Domains;
using DiabloII.Infrastructure.DbContext;

namespace DiabloII.Infrastructure.Repositories
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ErrorLogRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        #region Read
        public IReadOnlyCollection<ErrorLog> GetAll() => _dbContext.ErrorLogs
            .OrderBy(errorLog => errorLog.CreationDateUtc)
            .ToList();
        #endregion
    }
}