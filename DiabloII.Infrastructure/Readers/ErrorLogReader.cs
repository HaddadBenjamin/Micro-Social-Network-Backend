using System.Collections.Generic;
using DiabloII.Domain.Models.ErrorLogs;
using DiabloII.Domain.Readers.Domains;
using DiabloII.Domain.Repositories;

namespace DiabloII.Infrastructure.Readers
{
    public class ErrorLogReader : IErrorLogReader
    {
        private readonly IErrorLogRepository _repository;

        public ErrorLogReader(IErrorLogRepository repository) => _repository = repository;

        public IReadOnlyCollection<ErrorLog> GetAll() => _repository.GetAll();
    }
}