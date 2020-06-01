using System;
using MediatR;

namespace DiabloII.Domain.Commands.Suggestions
{
    public class CreateASuggestionCommand : IRequest<Guid>
    {
        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
