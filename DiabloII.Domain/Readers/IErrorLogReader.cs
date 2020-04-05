using System.Collections.Generic;
using DiabloII.Domain.Models.ErrorLogs;

namespace DiabloII.Domain.Readers
{
    public interface IErrorLogReader
    {
        void Log(ErrorLog errorLog);

        IReadOnlyCollection<ErrorLog> GetAll();
    }
}