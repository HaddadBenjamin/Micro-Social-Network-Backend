using System;
using System.Linq;
using AutoMapper;
using DiabloII.Items.Api.Application.Requests.Suggestions;
using DiabloII.Items.Api.Application.Responses.Suggestions;
using DiabloII.Items.Api.Domain.Models.Suggestions;

namespace DiabloII.Items.Api.Application.Mappers
{
    public class SuggestionMapper : Profile
    {
        public SuggestionMapper()
        {
            // Data layer to DTO layer.
            CreateMap<Suggestion, SuggestionDto>()
                .AfterMap((dataModel, dto) =>
                {
                    dto.PositiveVoteCount = dataModel.Votes.Count(vote => vote.IsPositive);
                    dto.NegativeVoteCount = dataModel.Votes.Count(vote => !vote.IsPositive);
                });
          
            CreateMap<SuggestionVote, SuggestionVoteDto>();
            CreateMap<SuggestionComment, SuggestionCommentDto>();

            // DTO layer to data layer.
            CreateMap<CreateASuggestionDto, Suggestion>().AfterMap((dataModel, dto) => dto.Id = Guid.NewGuid());
            CreateMap<VoteToASuggestionDto, SuggestionVote>().AfterMap((dataModel, dto) => dto.Id = Guid.NewGuid());
            CreateMap<CommentASuggestionDto, SuggestionComment>().AfterMap((dataModel, dto) => dto.Id = Guid.NewGuid());
        }
    }
}