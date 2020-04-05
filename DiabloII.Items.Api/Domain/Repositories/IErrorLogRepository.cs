using System.Collections.Generic;
using DiabloII.Items.Api.Domain.Models.ErrorLogs;

namespace DiabloII.Items.Api.Domain.Repositories
{
    public interface IErrorLogRepository
    {
        #region Read
        IReadOnlyCollection<ErrorLog> GetAll();
        #endregion
    }
}