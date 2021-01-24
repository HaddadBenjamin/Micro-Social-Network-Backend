using AutoMapper;
using DiabloII.Application.Responses.Read.ErrorLogs;
using DiabloII.Domain.Models.ErrorLogs;

namespace DiabloII.Application.Mappers.ErrorLogs
{
    public class ErrorLogDataToDtoLayer : Profile
    {
        public ErrorLogDataToDtoLayer()
        {
            CreateMap<ErrorLog, ErrorLogDto>();
        }
    }
}
