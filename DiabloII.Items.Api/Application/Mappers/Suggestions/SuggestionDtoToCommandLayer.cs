using AutoMapper;
using DiabloII.Items.Api.Application.Requests.Suggestions;
using DiabloII.Items.Api.Domain.Commands.Suggestions;

namespace DiabloII.Items.Api.Application.Mappers.Suggestions
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