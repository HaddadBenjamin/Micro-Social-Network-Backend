using AutoMapper;
using DiabloII.Items.Api.Application.Responses.ErrorLogs;
using DiabloII.Items.Api.Domain.Models.ErrorLogs;

namespace DiabloII.Items.Api.Application.Mappers
{
    public class ErrorLogMapper : Profile
    {
        public ErrorLogMapper()
        {
            // Data layer to DTO layer.
            CreateMap<ErrorLog, ErrorLogDto>();
        }
    }
}
