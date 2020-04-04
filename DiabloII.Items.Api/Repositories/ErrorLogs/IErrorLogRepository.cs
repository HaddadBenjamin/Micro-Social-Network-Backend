using System.Collections.Generic;
using DiabloII.Items.Api.DbContext.ErrorLogs.Models;

namespace DiabloII.Items.Api.Repositories.ErrorLogs
{
    public interface IErrorLogRepository
    {
        #region Read
        IReadOnlyCollection<ErrorLog> GetAll();
        #endregion
    }
}