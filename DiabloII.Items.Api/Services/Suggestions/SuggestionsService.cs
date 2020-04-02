using System.Collections.Generic;
using System.Linq;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Mappers.Suggestions;
using DiabloII.Items.Api.Requests.Suggestions;
using DiabloII.Items.Api.Responses.Suggestions;
using DiabloII.Items.Api.Validators.Suggestions.Create;
using DiabloII.Items.Api.Validators.Suggestions.Vote;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Items.Api.Services.Suggestions
{
    public class SuggestionsService : ISuggestionsService
    {
        private readonly ApplicationDbContext _dbContext;

        public SuggestionsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SuggestionDto Create(CreateASuggestionDto createASugestion)
        {
            var validationContext = new CreateASuggestionValidatorContext(createASugestion, _dbContext);
            var validator = new CreateASuggestionValidator();

            validator.Validate(validationContext);
           
            var suggestion = SuggestionMapper.ToSuggestion(createASugestion);

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SaveChanges();

            return SuggestionMapper.ToSuggestionDto(suggestion);
        }

        public SuggestionDto Vote(VoteToASuggestionDto voteToASuggestionDto)
        {
            var validationContext = new VoteToASuggestionValidatorContext(voteToASuggestionDto, _dbContext);
            var validator = new VoteToASuggestionValidator();

            validator.Validate(validationContext);

            var suggestion = _dbContext.Suggestions
                .Include(suggestionModel => suggestionModel.Votes)
                .First(vote => vote.Id == voteToASuggestionDto.SuggestionId);
            var suggestionVote = _dbContext.SuggestionVotes.FirstOrDefault(vote => vote.Ip == voteToASuggestionDto.Ip);
            var suggestionVoteExists = suggestionVote != null;
            
            if (suggestionVoteExists)
                suggestionVote.IsPositive = voteToASuggestionDto.IsPositive;
            else
            {
                suggestionVote = SuggestionMapper.ToSuggestionVote(voteToASuggestionDto);
             
                suggestion.Votes.Add(suggestionVote);
                _dbContext.SuggestionVotes.Add(suggestionVote);
            }

            _dbContext.SaveChanges();

            return SuggestionMapper.ToSuggestionDto(suggestion);
        }

        public IReadOnlyCollection<SuggestionDto> GetAll() => _dbContext.Suggestions
            .Include(suggestion => suggestion.Votes)
            .Select(SuggestionMapper.ToSuggestionDto)
            .ToList();
    }
}