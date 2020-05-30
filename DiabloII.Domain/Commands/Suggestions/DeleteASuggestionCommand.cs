using System;
using MediatR;

namespace DiabloII.Domain.Commands.Suggestions
{
    public class DeleteASuggestionCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }
    }
}
