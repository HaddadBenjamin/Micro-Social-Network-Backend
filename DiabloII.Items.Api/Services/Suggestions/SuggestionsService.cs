using System;
using System.Collections.Generic;
using System.Linq;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Suggestions.Models;
using DiabloII.Items.Api.Mappers.Suggestions;
using DiabloII.Items.Api.Requests.Suggestions;
using DiabloII.Items.Api.Validators.Suggestions.Comment;
using DiabloII.Items.Api.Validators.Suggestions.Create;
using DiabloII.Items.Api.Validators.Suggestions.Delete;
using DiabloII.Items.Api.Validators.Suggestions.DeleteAComment;
using DiabloII.Items.Api.Validators.Suggestions.Vote;

namespace DiabloII.Items.Api.Services.Suggestions
{
    public class SuggestionsService : ISuggestionsService
    {
        private readonly ApplicationDbContext _dbContext;

        public SuggestionsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Suggestion Create(CreateASuggestionDto createASugestion)
        {
            var validationContext = new CreateASuggestionValidationContext(createASugestion, _dbContext);
            var validator = new CreateASuggestionValidator();

            validator.Validate(validationContext);
           
            var suggestion = SuggestionMapper.ToSuggestion(createASugestion);

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SaveChanges();

            return suggestion;
        }

        public Suggestion Vote(VoteToASuggestionDto voteToASuggestionDto)
        {
            var validationContext = new VoteToASuggestionValidationContext(voteToASuggestionDto, _dbContext);
            var validator = new VoteToASuggestionValidator();

            validator.Validate(validationContext);

            //ISuggestionRepository.GetFirstWhereSuggestionIdEqualTo
            var suggestion = _dbContext
                .GetSuggestions()
                .First(vote => vote.Id == voteToASuggestionDto.SuggestionId);
            var suggestionVote = suggestion.Votes.FirstOrDefault(vote => 
                vote.Ip == voteToASuggestionDto.Ip && 
                vote.SuggestionId == voteToASuggestionDto.SuggestionId);
            var suggestionVoteExists = suggestionVote != null;

            if (suggestionVoteExists)
            {
                //ISuggestionRepository.RemoveVote
                suggestion.Votes.Remove(suggestionVote);
                _dbContext.SuggestionVotes.Remove(suggestionVote);
            }
            else
            {
                suggestionVote = SuggestionMapper.ToSuggestionVote(voteToASuggestionDto);
             
                //ISuggestionRepoistory.AddVote
                suggestion.Votes.Add(suggestionVote);
                _dbContext.SuggestionVotes.Add(suggestionVote);
            }

            _dbContext.SaveChanges();

            return suggestion;
        }

        public Suggestion Comment(CommentASuggestionDto commentASuggestion)
        {
            var validationContext = new CommentASuggestionValidationContext(commentASuggestion, _dbContext);
            var validator = new CommentASuggestionValidator();

            validator.Validate(validationContext);

            // ISuggestionRepository.GetFirstWhereSuggestionIdEqualTo
            var suggestion = _dbContext
                .GetSuggestions()
                .First(comment => comment.Id == commentASuggestion.SuggestionId);
            var suggestionComment = SuggestionMapper.ToSuggestionComment(commentASuggestion);

            //ISuggestionRepository.AddComment
            suggestion.Comments.Add(suggestionComment);
            _dbContext.SuggestionComments.Add(suggestionComment);
            _dbContext.SaveChanges();

            return suggestion;
        }

        public IReadOnlyCollection<Suggestion> GetAll() => _dbContext
            .GetSuggestions()
            .ToList();

        public Guid Delete(DeleteASuggestionDto deleteASuggestion)
        {
            var validationContext = new DeleteASuggestionValidationContext(deleteASuggestion, _dbContext);
            var validator = new DeleteASuggestionValidator();

            validator.Validate(validationContext);

            var suggestionToDelete = _dbContext.Suggestions.FirstOrDefault(suggestion => 
                suggestion.Ip == deleteASuggestion.Ip &&
                suggestion.Id == deleteASuggestion.Id);

            _dbContext.Remove(suggestionToDelete);
            _dbContext.SaveChanges();

            return deleteASuggestion.Id;
        }

        public Suggestion DeleteAComment(DeleteASuggestionCommentDto deleteASuggestionComment)
        {
            var validationContext = new DeleteASuggestionCommentValidationContext(deleteASuggestionComment, _dbContext);
            var validator = new DeleteASuggestionCommentValidator();

            validator.Validate(validationContext);

            //ISuggestionRepository.GetFirstWhereSuggestionIdEqualTo
            var suggestion = _dbContext.GetSuggestions().First(suggestionModel => suggestionModel.Id == deleteASuggestionComment.SuggestionId);
            //ISugestionRepository.GetMyComment(ip, id)
            var suggestionCommentToDelete = suggestion.Comments.First(comment =>
                comment.Ip == deleteASuggestionComment.Ip &&
                comment.Id == deleteASuggestionComment.Id);

            //ISuggestionRepository.RemoveComment
            suggestion.Comments.Remove(suggestionCommentToDelete);
            _dbContext.SuggestionComments.Remove(suggestionCommentToDelete);
            _dbContext.SaveChanges();

            return suggestion;
        }
    }
}