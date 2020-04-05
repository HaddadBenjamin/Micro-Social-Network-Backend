using System;
using AutoMapper;
using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Models.Suggestions;

namespace DiabloII.Domain.Mappers.Suggestions
{
    public class SuggestionCommandToDataLayer : Profile
    {
        public SuggestionCommandToDataLayer()
        {
            CreateMap<CreateASuggestionCommand, Suggestion>().AfterMap((dataModel, dto) => dto.Id = Guid.NewGuid());
            CreateMap<VoteToASuggestionCommand, SuggestionVote>().AfterMap((dataModel, dto) => dto.Id = Guid.NewGuid());
            CreateMap<CommentASuggestionCommand, SuggestionComment>().AfterMap((dataModel, dto) => dto.Id = Guid.NewGuid());
        }
    }
}
