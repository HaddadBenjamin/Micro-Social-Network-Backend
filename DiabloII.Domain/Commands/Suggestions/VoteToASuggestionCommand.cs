using System;
using MediatR;

namespace DiabloII.Domain.Commands.Suggestions
{
    public class VoteToASuggestionCommand : IRequest<Guid>
    {
        public Guid SuggestionId { get; set; }

        public bool IsPositive { get; set; }

        public string UserId { get; set; }
    }
}
