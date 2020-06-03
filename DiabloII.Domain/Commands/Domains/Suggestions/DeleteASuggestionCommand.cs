using System;
using DiabloII.Domain.Commands.Bases;
using MediatR;

namespace DiabloII.Domain.Commands.Domains.Suggestions
{
    public class DeleteASuggestionCommand : IRequest, IDeleteCommand
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }
    }
}
