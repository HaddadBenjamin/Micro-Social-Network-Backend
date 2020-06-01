using AutoMapper;
using DiabloII.Application.Requests.Write.Suggestions;
using DiabloII.Domain.Commands.Suggestions;

namespace DiabloII.Application.Mappers.Suggestions
{
    public class SuggestionDtoToCommandLayer : Profile
    {
        public SuggestionDtoToCommandLayer()
        {
            CreateMap<CreateASuggestionDto, CreateASuggestionCommand>();
            CreateMap<CommentASuggestionDto, CommentASuggestionCommand>();
            CreateMap<VoteToASuggestionDto, VoteToASuggestionCommand>();
            CreateMap<DeleteASuggestionDto, DeleteASuggestionCommand>();
            CreateMap<DeleteASuggestionCommentDto, DeleteASuggestionCommentCommand>();
        }
    }
}