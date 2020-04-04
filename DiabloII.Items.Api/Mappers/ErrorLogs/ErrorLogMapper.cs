using DiabloII.Items.Api.DbContext.ErrorLogs.Models;
using DiabloII.Items.Api.Responses.ErrorLogs;

namespace DiabloII.Items.Api.Mappers.ErrorLogs
{
    public static class ErrorLogMapper
    {
        public static ErrorLogDto ToErrorLogDto(ErrorLog errorLog) => new ErrorLogDto
        {
            CreationDateUtc = errorLog.CreationDateUtc,
            Content = errorLog.Content,
            Id = errorLog.Id
        };
    }
}
