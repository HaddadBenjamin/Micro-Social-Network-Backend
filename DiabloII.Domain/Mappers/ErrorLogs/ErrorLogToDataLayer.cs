using AutoMapper;
using DiabloII.Domain.Commands.Domains.ErrorLogs;
using DiabloII.Domain.Models.ErrorLogs;

namespace DiabloII.Domain.Mappers.ErrorLogs
{
    public class ErrorLogToDataLayer : Profile
    {
        public ErrorLogToDataLayer() => CreateMap<CreateErrorLogCommand, ErrorLog>();
    }
}
