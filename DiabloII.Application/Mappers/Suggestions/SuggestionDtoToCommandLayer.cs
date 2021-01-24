using System;
using AutoMapper;
using DiabloII.Application.Requests.Write.Suggestions;
using DiabloII.Domain.Commands.Domains.Suggestions;

namespace DiabloII.Application.Mappers.Suggestions
{
    public class SuggestionDtoToCommandLayer : Profile
    {
        public SuggestionDtoToCommandLayer()
        {
            CreateMap<CreateASuggestionDto, CreateASuggestionCommand>().AfterMap((dto, command) => command.Id = Guid.NewGuid());
            CreateMap<CommentASuggestionDto, CommentASuggestionCommand>().AfterMap((dto, command) => command.Id = Guid.NewGuid());
            CreateMap<VoteToASuggestionDto, VoteToASuggestionCommand>().AfterMap((dto, command) => command.Id = Guid.NewGuid());
            CreateMap<DeleteASuggestionDto, DeleteASuggestionCommand>();
            CreateMap<DeleteASuggestionCommentDto, DeleteASuggestionCommentCommand>();
        }
    }
}