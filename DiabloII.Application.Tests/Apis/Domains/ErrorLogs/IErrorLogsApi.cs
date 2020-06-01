using DiabloII.Application.Responses.Read.ErrorLogs;
using DiabloII.Application.Tests.Apis.Bases;

namespace DiabloII.Application.Tests.Apis.Domains.ErrorLogs
{
    public interface IErrorLogsApi : IApiGetAll<ErrorLogDto>
    {
    }
}