using System.Collections.Generic;
using System.Linq;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.ErrorLogs.Models;

namespace DiabloII.Items.Api.Repositories.ErrorLogs
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ErrorLogRepository(ApplicationDbContext dbContext) =>_dbContext = dbContext;

        #region Read
        public IReadOnlyCollection<ErrorLog> GetAll() => _dbContext.ErrorLogs
            .OrderBy(errorLog => errorLog.CreationDateUtc)
            .ToList();
        #endregion
    }
}