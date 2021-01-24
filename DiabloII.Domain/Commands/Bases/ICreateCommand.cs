using System;

namespace DiabloII.Domain.Commands.Bases
{
    public interface ICreateCommand : ICreateCommand<Guid>
    {
    }

    public interface ICreateCommand<IdType>
    {
        IdType Id { get; set; }
    }
}
