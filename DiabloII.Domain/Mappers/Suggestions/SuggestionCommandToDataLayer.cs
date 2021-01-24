﻿using AutoMapper;
using DiabloII.Domain.Commands.Domains.Suggestions;
using DiabloII.Domain.Models.Suggestions;

namespace DiabloII.Domain.Mappers.Suggestions
{
    public class SuggestionCommandToDataLayer : Profile
    {
        public SuggestionCommandToDataLayer()
        {
            CreateMap<CreateASuggestionCommand, Suggestion>().AfterMap((command, dataModel) =>
                dataModel.CreatedBy = command.UserId);
            CreateMap<VoteToASuggestionCommand, SuggestionVote>().AfterMap((command, dataModel) =>
                dataModel.CreatedBy = command.UserId);
            CreateMap<CommentASuggestionCommand, SuggestionComment>().AfterMap((command, dataModel) =>
                dataModel.CreatedBy = command.UserId);
        }
    }
}
