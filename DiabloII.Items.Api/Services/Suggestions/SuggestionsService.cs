using System;
using System.Collections.Generic;
using System.Linq;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Mappers.Suggestions;
using DiabloII.Items.Api.Requests.Suggestions;
using DiabloII.Items.Api.Responses.Suggestions;
using DiabloII.Items.Api.Validators.Suggestions.Create;
using DiabloII.Items.Api.Validators.Suggestions.Delete;
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

            var suggestion = _dbContext
                .GetSuggestions()
                .First(vote => vote.Id == voteToASuggestionDto.SuggestionId);
            var suggestionVote = suggestion.Votes.FirstOrDefault(vote => 
                vote.Ip == voteToASuggestionDto.Ip && 
                vote.Suggestion.Id == voteToASuggestionDto.SuggestionId);
            var suggestionVoteExists = suggestionVote != null;

            if (suggestionVoteExists)
            {
                suggestion.Votes.Remove(suggestionVote);
                _dbContext.SuggestionVotes.Remove(suggestionVote);
            }
            else
            {
                suggestionVote = SuggestionMapper.ToSuggestionVote(voteToASuggestionDto);
             
                suggestion.Votes.Add(suggestionVote);
                _dbContext.SuggestionVotes.Add(suggestionVote);
            }

            _dbContext.SaveChanges();

            return SuggestionMapper.ToSuggestionDto(suggestion);
        }

        public SuggestionDto Comment(CommentASuggestionDto commentASuggestion)
        {
            var suggestion = _dbContext
                .GetSuggestions()
                .First(comment => comment.Id == commentASuggestion.SuggestionId);
            var suggestionComment = SuggestionMapper.ToSuggestionComment(commentASuggestion);

            suggestion.Comments.Add(suggestionComment);
            _dbContext.SuggestionComments.Add(suggestionComment);
            _dbContext.SaveChanges();

            return SuggestionMapper.ToSuggestionDto(suggestion);
        }

        public IReadOnlyCollection<SuggestionDto> GetAll() => _dbContext
            .GetSuggestions()
            .Select(SuggestionMapper.ToSuggestionDto)
            .ToList();

        public Guid Delete(DeleteASuggestionDto deleteASuggestion)
        {
            var validationContext = new DeleteASuggestionValidatorContext(deleteASuggestion, _dbContext);
            var validator = new DeleteASuggestionValidator();

            validator.Validate(validationContext);

            var suggestionToDelete = _dbContext.Suggestions.FirstOrDefault(suggestion => 
                suggestion.Ip == deleteASuggestion.Ip &&
                suggestion.Id == deleteASuggestion.Id);

            _dbContext.Remove(suggestionToDelete);
            _dbContext.SaveChanges();

            return deleteASuggestion.Id;
        }
    }
}