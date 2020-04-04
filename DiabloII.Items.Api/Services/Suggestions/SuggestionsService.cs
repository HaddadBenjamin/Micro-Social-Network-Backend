using System;
using System.Collections.Generic;
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
        private readonly ISuggestionRepository _repository;

        public SuggestionsService(ApplicationDbContext dbContext, ISuggestionRepository repository)
        {
            _dbContext = dbContext;
            _repository = repository;
        }

        #region Read
        public IReadOnlyCollection<Suggestion> GetAll() => _repository.GetAll();
        #endregion

        #region Write
        public Suggestion Create(CreateASuggestionDto createASugestion)
        {
            var validationContext = new CreateASuggestionValidationContext(createASugestion, _repository);
            var validator = new CreateASuggestionValidator();

            validator.Validate(validationContext);
           
            var suggestion = SuggestionMapper.ToSuggestion(createASugestion);

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SaveChanges();

            return suggestion;
        }

        public Suggestion Vote(VoteToASuggestionDto voteToASuggestionDto)
        {
            var validationContext = new VoteToASuggestionValidationContext(voteToASuggestionDto, _repository);
            var validator = new VoteToASuggestionValidator();

            validator.Validate(validationContext);

            var suggestion = _repository.GetFirstSuggestion(voteToASuggestionDto.SuggestionId);
            var suggestionVote = _repository.GetUserVoteOrDefault(suggestion, voteToASuggestionDto.Ip);
            var suggestionVoteExists = suggestionVote != null;

            if (suggestionVoteExists)
                _repository.RemoveVote(suggestion, suggestionVote);
            else
            {
                suggestionVote = SuggestionMapper.ToSuggestionVote(voteToASuggestionDto);
             
                _repository.AddVote(suggestion, suggestionVote);
            }

            _dbContext.SaveChanges();

            return suggestion;
        }

        public Suggestion Comment(CommentASuggestionDto commentASuggestion)
        {
            var validationContext = new CommentASuggestionValidationContext(commentASuggestion, _repository);
            var validator = new CommentASuggestionValidator();

            validator.Validate(validationContext);

            var suggestionComment = SuggestionMapper.ToSuggestionComment(commentASuggestion);
            var suggestion = _repository.AddComment(commentASuggestion.SuggestionId, suggestionComment);
            
            _dbContext.SaveChanges();

            return suggestion;
        }

        public Guid Delete(DeleteASuggestionDto deleteASuggestion)
        {
            var validationContext = new DeleteASuggestionValidationContext(deleteASuggestion, _repository);
            var validator = new DeleteASuggestionValidator();

            validator.Validate(validationContext);

            _repository.RemoveSuggestion(deleteASuggestion.Id, deleteASuggestion.Ip);
            _dbContext.SaveChanges();

            return deleteASuggestion.Id;
        }

        public Suggestion DeleteAComment(DeleteASuggestionCommentDto deleteASuggestionComment)
        {
            var validationContext = new DeleteASuggestionCommentValidationContext(deleteASuggestionComment, _repository);
            var validator = new DeleteASuggestionCommentValidator();

            validator.Validate(validationContext);

            var suggestion = _repository.RemoveComment(deleteASuggestionComment.SuggestionId, deleteASuggestionComment.Id, deleteASuggestionComment.Ip);
            
            _dbContext.SaveChanges();

            return suggestion;
        }
        #endregion
    }
}