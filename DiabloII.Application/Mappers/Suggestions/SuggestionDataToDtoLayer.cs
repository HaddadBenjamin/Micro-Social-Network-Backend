using System.Linq;
using AutoMapper;
using DiabloII.Application.Responses.Read.Suggestions;
using DiabloII.Domain.Models.Suggestions;

namespace DiabloII.Application.Mappers.Suggestions
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