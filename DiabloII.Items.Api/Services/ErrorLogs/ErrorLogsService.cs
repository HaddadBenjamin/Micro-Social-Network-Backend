﻿using System.Collections.Generic;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.ErrorLogs.Models;
using DiabloII.Items.Api.Repositories.ErrorLogs;

namespace DiabloII.Items.Api.Services.ErrorLogs
{
    public class ErrorLogsService : IErrorLogsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IErrorLogRepository _repository;

        public ErrorLogsService(ApplicationDbContext dbContext, IErrorLogRepository repository)
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