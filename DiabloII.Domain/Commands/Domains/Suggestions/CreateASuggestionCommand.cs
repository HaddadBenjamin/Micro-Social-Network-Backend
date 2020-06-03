using System;
using DiabloII.Domain.Commands.Bases;
using MediatR;

namespace DiabloII.Domain.Commands.Domains.Suggestions
{
    public class CreateASuggestionCommand : IRequest, ICreateCommand
    {
        public string Content { get; set; }

        public string UserId { get; set; }

        public Guid Id { get; set; }
    }
}
