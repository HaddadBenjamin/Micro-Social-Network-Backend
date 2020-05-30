using System;
using DiabloII.Domain.Models.Suggestions;
using MediatR;

namespace DiabloII.Domain.Commands.Suggestions
{
    public class DeleteASuggestionCommentCommand : IRequest<Suggestion>
    {
        public Guid Id { get; set; }

        public Guid SuggestionId { get; set; }

        public string UserId { get; set; }
    }
}
