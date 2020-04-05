using AutoMapper;
using DiabloII.Items.Api.Application.Responses.ErrorLogs;
using DiabloII.Items.Api.Domain.Models.ErrorLogs;

namespace DiabloII.Items.Api.Application.Profiles
{
    public class ErrorLogMappingConfiguration : Profile
    {
        public ErrorLogMappingConfiguration()
        {

            CreateMap<ErrorLog, ErrorLogDto>();
        }
    }
}
