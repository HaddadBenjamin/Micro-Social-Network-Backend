using System;
using DiabloII.Domain.Models.Suggestions;
using MediatR;

namespace DiabloII.Domain.Commands.Suggestions
{
    public class CommentASuggestionCommand : IRequest<Suggestion>
    {
        public Guid SuggestionId { get; set; }

        public string UserId { get; set; }

        public string Comment { get; set; }
    }
}
