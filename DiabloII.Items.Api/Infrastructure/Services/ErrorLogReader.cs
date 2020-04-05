using System.Collections.Generic;
using DiabloII.Items.Api.Domain.Models.ErrorLogs;
using DiabloII.Items.Api.Domain.Readers;
using DiabloII.Items.Api.Domain.Repositories;
using DiabloII.Items.Api.Infrastructure.DbContext;

namespace DiabloII.Items.Api.Infrastructure.Services
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