using System.Collections.Generic;
using DiabloII.Items.Api.Domain.Models.ErrorLogs;

namespace DiabloII.Items.Api.Application.Services.ErrorLogs
{
    public interface IErrorLogsService
    {
        void Log(ErrorLog errorLog);

        IReadOnlyCollection<ErrorLog> GetAll();
    }
}