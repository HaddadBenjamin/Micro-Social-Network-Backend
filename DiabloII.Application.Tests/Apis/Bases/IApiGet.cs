using System;
using System.Threading.Tasks;

namespace DiabloII.Application.Tests.Apis.Bases
{
    public interface IApiGet<ResponseDto> : IApiGet<ResponseDto, Guid>
    {
    }

    public interface IApiGet<ResponseDto, Parameters>
    {
        Task<ResponseDto> Get(Parameters suggestionId);
    }
}