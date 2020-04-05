using System;
using AutoMapper;
using DiabloII.Items.Api.Domain.Commands.Suggestions;
using DiabloII.Items.Api.Domain.Models.Suggestions;

namespace DiabloII.Items.Api.Domain.Mappers.Suggestions
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
