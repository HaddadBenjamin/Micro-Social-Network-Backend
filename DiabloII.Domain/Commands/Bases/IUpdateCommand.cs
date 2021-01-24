using System;

namespace DiabloII.Domain.Commands.Bases
{
    public interface IUpdateCommand : IUpdateCommand<Guid>
    {
    }

    public interface IUpdateCommand<IdType>
    {
        IdType Id { get; set; }
    }
}