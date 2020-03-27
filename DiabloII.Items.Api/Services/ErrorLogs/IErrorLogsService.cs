using System.Collections.Generic;
using DiabloII.Items.Api.DbContext.Suggestions;

namespace DiabloII.Items.Api
{
    public interface IErrorLogsService
    {
        void Log(ErrorLog errorLog);

        IReadOnlyCollection<ErrorLog> GetAll();
    }
}