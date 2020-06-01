using DiabloII.Application.Responses.Read.Domains.ErrorLogs;
using DiabloII.Application.Tests.Apis.Bases;

namespace DiabloII.Application.Tests.Apis.Domains.ErrorLogs
{
    public interface IErrorLogsApi : IApiGetAll<ErrorLogDto>
    {
    }
}