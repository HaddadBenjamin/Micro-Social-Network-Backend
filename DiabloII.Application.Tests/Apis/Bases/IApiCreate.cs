using System;
using System.Threading.Tasks;

namespace DiabloII.Application.Tests.Apis.Bases
{
    public interface IApiCreate<CreateDto> : IApiCreate<CreateDto, Guid>
    {
    }

    public interface IApiCreate<CreateDto, RestResourceId>
    {
        Task<RestResourceId> Create(CreateDto dto);
    }
}