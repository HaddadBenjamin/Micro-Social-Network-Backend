using System;
using AutoMapper;
using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Handlers;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Validations.Suggestions.Comment;
using DiabloII.Domain.Validations.Suggestions.Create;
using DiabloII.Domain.Validations.Suggestions.Delete;
using DiabloII.Domain.Validations.Suggestions.DeleteAComment;
using DiabloII.Domain.Validations.Suggestions.Vote;
using DiabloII.Infrastructure.DbContext;

namespace DiabloII.Infrastructure.Handlers
{
    public class SuggestionCommandHandler : ISuggestionCommandHandler
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ISuggestionRepository _repository;
        private readonly IMapper _mapper;
        private readonly CreateASuggestionValidator _createASuggestionValidator;
        private readonly VoteToASuggestionValidator _voteToASuggestionValidator;
        private readonly CommentASuggestionValidator _commentASuggestionValidator;
        private readonly DeleteASuggestionValidator _deleteASuggestionValidator;
        private readonly DeleteASuggestionCommentValidator _deleteASuggestionCommentValidator;

        public SuggestionCommandHandler(
            ApplicationDbContext dbContext,
            ISuggestionRepository repository,
            IMapper mapper,
            CreateASuggestionValidator createASuggestionValidator,
            VoteToASuggestionValidator voteToASuggestionValidator,
            CommentASuggestionValidator commentASuggestionValidator,
            DeleteASuggestionValidator deleteASuggestionValidator,
            DeleteASuggestionCommentValidator deleteASuggestionCommentValidator)
        {
            _dbContext = dbContext;
            _repository = repository;
            _mapper = mapper;
            _createASuggestionValidator = createASuggestionValidator;
            _voteToASuggestionValidator = voteToASuggestionValidator;
            _commentASuggestionValidator = commentASuggestionValidator;
            _deleteASuggestionValidator = deleteASuggestionValidator;
            _deleteASuggestionCommentValidator = deleteASuggestionCommentValidator;
        }


        #region Write
        public Suggestion Create(CreateASuggestionCommand createASugestion)
        {
            var validationContext = new CreateASuggestionValidationContext(createASugestion, _repository);

            _createASuggestionValidator.Validate(validationContext);

            var suggestion = _mapper.Map<Suggestion>(createASugestion);

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SaveChanges();

            return suggestion;
        }

        public Suggestion Vote(VoteToASuggestionCommand voteToASuggestionCommand)
        {
            var validationContext = new VoteToASuggestionValidationContext(voteToASuggestionCommand, _repository);

            _voteToASuggestionValidator.Validate(validationContext);

            var suggestion = _repository.GetFirstSuggestion(voteToASuggestionCommand.SuggestionId);
            var suggestionVote = _repository.GetUserVoteOrDefault(suggestion, voteToASuggestionCommand.Ip);
            var suggestionVoteExists = suggestionVote != null;

            if (suggestionVoteExists)
                _repository.RemoveVote(suggestion, suggestionVote);
            else
            {
                suggestionVote = _mapper.Map<SuggestionVote>(voteToASuggestionCommand);

                _repository.AddVote(suggestion, suggestionVote);
            }

            _dbContext.SaveChanges();

            return suggestion;
        }

        public Suggestion Comment(CommentASuggestionCommand commentASuggestion)
        {
            var validationContext = new CommentASuggestionValidationContext(commentASuggestion, _repository);

            _commentASuggestionValidator.Validate(validationContext);

            var suggestionComment = _mapper.Map<SuggestionComment>(commentASuggestion);
            var suggestion = _repository.AddComment(commentASuggestion.SuggestionId, suggestionComment);

            _dbContext.SaveChanges();

            return suggestion;
        }

        public Guid Delete(DeleteASuggestionCommand deleteASuggestion)
        {
            var validationContext = new DeleteASuggestionValidationContext(deleteASuggestion, _repository);

            _deleteASuggestionValidator.Validate(validationContext);

            _repository.RemoveSuggestion(deleteASuggestion.Id, deleteASuggestion.Ip);
            _dbContext.SaveChanges();

            return deleteASuggestion.Id;
        }

        public Suggestion DeleteAComment(DeleteASuggestionCommentCommand deleteASuggestionComment)
        {
            var validationContext = new DeleteASuggestionCommentValidationContext(deleteASuggestionComment, _repository);

            _deleteASuggestionCommentValidator.Validate(validationContext);

            var suggestion = _repository.RemoveComment(deleteASuggestionComment.SuggestionId, deleteASuggestionComment.Id, deleteASuggestionComment.Ip);

            _dbContext.SaveChanges();

            return suggestion;
        }
        #endregion
    }
}