using System.Collections.Generic;
using System.Linq;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Queries.Suggestions;
using DiabloII.Items.Api.Responses.Suggestions;
using DiabloII.Items.Api.Validators.Suggestions;
using DiabloII.Items.Api.Validators.Suggestions.Create;
using DiabloII.Items.Api.Validators.Suggestions.Vote;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Items.Api.Services.Suggestions
{
    public class SuggestionsService : ISuggestionsService
    {
        public void Create(CreateASuggestionDto createASugestion, ApplicationDbContext dbContext)
        {
            var validationContext = new CreateASuggestionValidatorContext(createASugestion, dbContext);
            var validator = new CreateASuggestionValidator();

            validator.Validate(validationContext);
           
            var suggestion = SuggestionMapper.ToSuggestion(createASugestion);

            dbContext.Suggestions.Add(suggestion);
            dbContext.SaveChanges();
        }

        public SuggestionDto Vote(VoteToASuggestionDto voteToASuggestionDto, ApplicationDbContext dbContext)
        {
            var validationContext = new VoteToASuggestionValidatorContext(voteToASuggestionDto, dbContext);
            var validator = new VoteToASuggestionValidator();

            validator.Validate(validationContext);

            var suggestion = dbContext.Suggestions.First(vote => vote.Id == voteToASuggestionDto.SuggestionId);
            var suggestionVote = dbContext.SuggestionVotes.FirstOrDefault(vote => vote.Ip == voteToASuggestionDto.Ip);
            var suggestionVoteExists = suggestionVote != null;
            
            if (suggestionVoteExists)
                suggestionVote.IsPositive = voteToASuggestionDto.IsPositive;
            else
            {
                suggestionVote = SuggestionMapper.ToSuggestionVote(voteToASuggestionDto);

                dbContext.SuggestionVotes.Add(suggestionVote);
            }

            dbContext.SaveChanges();

            return SuggestionMapper.ToSuggestionDto(suggestion);
        }

        public IReadOnlyCollection<SuggestionDto> GetAll(ApplicationDbContext dbContext)
            => dbContext.Suggestions
                .Include(suggestion => suggestion.Votes)
                .Select(SuggestionMapper.ToSuggestionDto)
                .ToList();
    }
}