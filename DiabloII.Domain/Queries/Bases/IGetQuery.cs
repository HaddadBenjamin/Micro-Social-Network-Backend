using System;

namespace DiabloII.Domain.Queries.Bases
{
    public interface IGetQuery : IGetQuery<Guid> { }

    public interface IGetQuery<IdType>
    {
        IdType Id { get; set; }
    }
}
