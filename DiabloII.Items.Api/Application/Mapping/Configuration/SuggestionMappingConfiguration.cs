using System;
using System.Linq;
using AutoMapper;
using DiabloII.Items.Api.Application.Requests.Suggestions;
using DiabloII.Items.Api.Application.Responses.Suggestions;
using DiabloII.Items.Api.Domain.Models.Suggestions;

namespace DiabloII.Items.Api.Application.Profiles
{
    public class SuggestionMappingConfiguration : Profile
    {
        public SuggestionMappingConfiguration()
        {
            CreateMap<Suggestion, SuggestionDto>()
                .AfterMap((dataModel, dto) =>
                {
                    dto.PositiveVoteCount = dataModel.Votes.Count(vote => vote.IsPositive);
                    dto.NegativeVoteCount = dataModel.Votes.Count(vote => !vote.IsPositive);
                });
          
            CreateMap<SuggestionVote, SuggestionVoteDto>();
            CreateMap<SuggestionComment, SuggestionCommentDto>();


            CreateMap<CreateASuggestionDto, Suggestion>().AfterMap((dataModel, dto) => dto.Id = Guid.NewGuid());
            CreateMap<VoteToASuggestionDto, SuggestionVote>().AfterMap((dataModel, dto) => dto.Id = Guid.NewGuid());
            CreateMap<CommentASuggestionDto, SuggestionComment>().AfterMap((dataModel, dto) => dto.Id = Guid.NewGuid());
        }
    }
}