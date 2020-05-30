using System;
using DiabloII.Domain.Models.Suggestions;
using MediatR;

namespace DiabloII.Domain.Commands.Suggestions
{
    public class VoteToASuggestionCommand : IRequest<Suggestion>
    {
        public Guid SuggestionId { get; set; }

        public bool IsPositive { get; set; }

        public string UserId { get; set; }
    }
}
