using System.Collections.Generic;
using DiabloII.Items.Api.Domain.Models.ErrorLogs;

namespace DiabloII.Items.Api.Infrastructure.Repositories.ErrorLogs
{
    public interface IErrorLogRepository
    {
        #region Read
        IReadOnlyCollection<ErrorLog> GetAll();
        #endregion
    }
}