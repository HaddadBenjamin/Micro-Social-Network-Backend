using System.Collections.Generic;
using DiabloII.Domain.Models.ErrorLogs;
using DiabloII.Domain.Readers;
using DiabloII.Domain.Repositories;
using DiabloII.Infrastructure.DbContext;

namespace DiabloII.Infrastructure.Readers
{
    public class ErrorLogReader : IErrorLogReader
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IErrorLogRepository _repository;

        public ErrorLogReader(ApplicationDbContext dbContext, IErrorLogRepository repository)
        {
            _dbContext = dbContext;
            _repository = repository;
        }

        #region Read
        public IReadOnlyCollection<ErrorLog> GetAll() => _repository.GetAll();
        #endregion

        #region Write
        public void Log(ErrorLog errorLog)
        {
            _dbContext.ErrorLogs.Add(errorLog);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}