using System.Collections.Generic;
using System.Linq;
using DiabloII.Items.Api.Domain.Models.ErrorLogs;
using DiabloII.Items.Api.Domain.Repositories;
using DiabloII.Items.Api.Infrastructure.DbContext;

namespace DiabloII.Items.Api.Infrastructure.Repositories
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