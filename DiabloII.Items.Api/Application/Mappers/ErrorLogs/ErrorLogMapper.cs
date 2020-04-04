using DiabloII.Items.Api.Application.Responses.ErrorLogs;
using DiabloII.Items.Api.Domain.Models.ErrorLogs;

namespace DiabloII.Items.Api.Application.Mappers.ErrorLogs
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
