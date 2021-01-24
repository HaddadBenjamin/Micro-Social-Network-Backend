using System;
using DiabloII.Domain.Commands.Bases;
using MediatR;

namespace DiabloII.Domain.Commands.Domains.Suggestions
{
    public class CommentASuggestionCommand : IRequest, ICreateCommand
    {
        public Guid SuggestionId { get; set; }

        public string UserId { get; set; }

        public string Comment { get; set; }

        public Guid Id { get; set; }
    }
}
