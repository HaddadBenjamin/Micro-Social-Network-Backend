using System.Linq;
using AutoMapper;
using DiabloII.Items.Api.Application.Responses.Suggestions;
using DiabloII.Items.Api.Domain.Models.Suggestions;

namespace DiabloII.Items.Api.Application.Mappers.Suggestions
{
    public class SuggestionDataToDtoLayer : Profile
    {
        public SuggestionDataToDtoLayer()
        {
            CreateMap<Suggestion, SuggestionDto>()
                .AfterMap((dataModel, dto) =>
                {
                    dto.PositiveVoteCount = dataModel.Votes.Count(vote => vote.IsPositive);
                    dto.NegativeVoteCount = dataModel.Votes.Count(vote => !vote.IsPositive);
                });

            CreateMap<SuggestionVote, SuggestionVoteDto>();
            CreateMap<SuggestionComment, SuggestionCommentDto>();
        }
    }
}