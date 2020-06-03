using System;
using DiabloII.Domain.Commands.Bases;
using MediatR;

namespace DiabloII.Domain.Commands.Domains.Suggestions
{
    public class DeleteASuggestionCommentCommand : IRequest, IDeleteCommand
    {
        public Guid Id { get; set; }

        public Guid SuggestionId { get; set; }

        public string UserId { get; set; }
    }
}
