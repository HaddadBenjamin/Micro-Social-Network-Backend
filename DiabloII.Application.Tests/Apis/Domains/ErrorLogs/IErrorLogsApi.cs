using DiabloII.Application.Responses.ErrorLogs;
using DiabloII.Application.Tests.Apis.Bases;

namespace DiabloII.Application.Tests.Apis.Domains.ErrorLogs
{
    public interface IErrorLogsApi : IGetAllApi<ErrorLogDto>
    {
    }
}