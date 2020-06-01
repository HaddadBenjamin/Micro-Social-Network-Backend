using System.Collections.Generic;
using DiabloII.Domain.Models.ErrorLogs;
using DiabloII.Domain.Readers;
using DiabloII.Domain.Readers.Domains;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Repositories.Domains;

namespace DiabloII.Infrastructure.Readers
{
    public class ErrorLogReader : IErrorLogReader
    {
        private readonly IErrorLogRepository _repository;

        public ErrorLogReader(IErrorLogRepository repository) => _repository = repository;

        public IReadOnlyCollection<ErrorLog> GetAll() => _repository.GetAll();
    }
}