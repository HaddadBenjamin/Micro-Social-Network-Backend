using DiabloII.Domain.Models.ErrorLogs;
using DiabloII.Domain.Repositories.Bases;

namespace DiabloII.Domain.Repositories.Domains
{
    public interface IErrorLogRepository : IRepositoryGetAll<ErrorLog>
    {
    }
}