using DiabloII.Domain.Models.Suggestions;
using MediatR;

namespace DiabloII.Domain.Commands.Suggestions
{
    public class CreateASuggestionCommand : IRequest<Suggestion>
    {
        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
