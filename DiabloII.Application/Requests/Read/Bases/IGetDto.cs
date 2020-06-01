using System;

namespace DiabloII.Application.Requests.Read.Bases
{
    public interface IGetDto : IGetDto<Guid>
    {
    }

    public interface IGetDto<IdType>
    {
        IdType Id { get; set; }
    }
}
