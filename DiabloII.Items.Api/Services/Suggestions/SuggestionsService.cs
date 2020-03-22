using System.Collections.Generic;
using System.Linq;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Queries.Suggestions;
using DiabloII.Items.Api.Responses.Suggestions;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Items.Api.Services.Suggestions
{
    public class SuggestionsService : ISuggestionsService
    {
        public void Create(CreateSuggestionDto createSugestion, ApplicationDbContext dbContext)
        {
            var suggestion = SuggestionMapper.ToSuggestion(createSugestion);

            dbContext.Suggestions.Add(suggestion);
            dbContext.SaveChanges();
        }

        public SuggestionDto Vote(SuggestionVoteDto suggestionVoteDto, ApplicationDbContext dbContext)
        {
            var suggestionVote = dbContext.SuggestionVotes.First(vote => vote.Ip == suggestionVoteDto.Ip);
            var suggestionVoteExists = suggestionVote != null;

            if (suggestionVoteExists)
                suggestionVote.IsPositive = suggestionVoteDto.IsPositive;
            else
            {
                suggestionVote = SuggestionMapper.ToSuggestionVote(suggestionVoteDto);

                dbContext.SuggestionVotes.Add(suggestionVote);
            }

            dbContext.SaveChanges();

            var suggestion = dbContext.Suggestions.First(vote => vote.Id == suggestionVote.SuggestionId);

            return SuggestionMapper.ToSuggestionDto(suggestion);
        }

        public IReadOnlyCollection<SuggestionDto> GetAll(ApplicationDbContext dbContext)
            => dbContext.Suggestions
                .Include(suggestion => suggestion.Votes)
                .Select(SuggestionMapper.ToSuggestionDto)
                .ToList();
    }
}