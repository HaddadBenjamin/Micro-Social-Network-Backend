using System.Collections.Generic;
using DiabloII.Domain.Models.ErrorLogs;

namespace DiabloII.Domain.Repositories
{
    public interface IErrorLogRepository
    {
        #region Read
        IReadOnlyCollection<ErrorLog> GetAll();
        #endregion
    }
}