using AutoMapper;
using DiabloII.Items.Api.Application.Responses.ErrorLogs;
using DiabloII.Items.Api.Domain.Models.ErrorLogs;

namespace DiabloII.Items.Api.Application.Mappers.ErrorLogs
{
    public class ErrorLogDataToDtoLayer : Profile
    {
        public ErrorLogDataToDtoLayer()
        {
            CreateMap<ErrorLog, ErrorLogDto>();
        }
    }
}
