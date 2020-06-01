using System;
using MediatR;

namespace DiabloII.Domain.Commands.Suggestions
{
    public class CommentASuggestionCommand : IRequest<Guid>
    {
        public Guid SuggestionId { get; set; }

        public string UserId { get; set; }

        public string Comment { get; set; }
    }
}
