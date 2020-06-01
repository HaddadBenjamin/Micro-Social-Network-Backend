using System;
using MediatR;

namespace DiabloII.Domain.Commands.ErrorLogs
{
    public class CreateErrorLogCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public DateTime CreationDateUtc { get; set; }

        public string Content { get; set; }
    }
}