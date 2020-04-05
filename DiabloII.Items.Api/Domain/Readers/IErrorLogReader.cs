using System.Collections.Generic;
using DiabloII.Items.Api.Domain.Models.ErrorLogs;

namespace DiabloII.Items.Api.Domain.Readers
{
    public interface IErrorLogReader
    {
        void Log(ErrorLog errorLog);

        IReadOnlyCollection<ErrorLog> GetAll();
    }
}