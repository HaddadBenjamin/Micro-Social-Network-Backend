using System;
using DiabloII.Domain.Commands.Bases;
using MediatR;

namespace DiabloII.Domain.Commands.Domains.ErrorLogs
{
    public class CreateErrorLogCommand : IRequest, ICreateCommand
    {
        public Guid Id { get; set; }

        public DateTime CreationDateUtc { get; set; }

        public string Content { get; set; }
    }
}