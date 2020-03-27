using System.Collections.Generic;
using DiabloII.Items.Api.DbContext.ErrorLogs.Models;

namespace DiabloII.Items.Api.Services.ErrorLogs
{
    public interface IErrorLogsService
    {
        void Log(ErrorLog errorLog);

        IReadOnlyCollection<ErrorLog> GetAll();
    }
}