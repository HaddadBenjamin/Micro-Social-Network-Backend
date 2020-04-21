using DiabloII.Domain.Models.ErrorLogs;
using DiabloII.Domain.Repositories.Bases;

namespace DiabloII.Domain.Repositories
{
    public interface IErrorLogRepository : IGetAllRepository<ErrorLog>
    {
    }
}