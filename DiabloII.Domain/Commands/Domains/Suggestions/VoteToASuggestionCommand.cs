using System;
using DiabloII.Domain.Commands.Bases;
using MediatR;

namespace DiabloII.Domain.Commands.Domains.Suggestions
{
    public class VoteToASuggestionCommand : IRequest, ICreateCommand
    {
        public Guid SuggestionId { get; set; }

        public bool IsPositive { get; set; }

        public string UserId { get; set; }

        public Guid Id { get; set; }
    }
}
