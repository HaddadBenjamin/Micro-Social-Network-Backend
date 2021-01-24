using System;

namespace DiabloII.Domain.Commands.Bases
{
    public interface IDeleteCommand : IDeleteCommand<Guid>
    {
    }

    public interface IDeleteCommand<IdType>
    {
        IdType Id { get; set; }
    }
}