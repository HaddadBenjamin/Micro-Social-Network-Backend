using System;
using MediatR;

namespace DiabloII.Domain.Commands.Suggestions
{
    public class DeleteASuggestionCommentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public Guid SuggestionId { get; set; }

        public string UserId { get; set; }
    }
}
