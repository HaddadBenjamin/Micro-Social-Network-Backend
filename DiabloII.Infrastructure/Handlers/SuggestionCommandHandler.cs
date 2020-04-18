using System;
using AutoMapper;
using DiabloII.Domain.Commands.Notifications;
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
        private readonly INotificationCommandHandler _notificationHandler;
        private readonly CreateASuggestionValidator _createValidator;
        private readonly VoteToASuggestionValidator _voteValidator;
        private readonly CommentASuggestionValidator _commentValidator;
        private readonly DeleteASuggestionValidator _deleteValidator;
        private readonly DeleteASuggestionCommentValidator _deleteCommentValidator;

        public SuggestionCommandHandler(
            ISuggestionRepository repository,
            IMapper mapper,
            INotificationCommandHandler notificationHandler,
            ApplicationDbContext dbContext,
            CreateASuggestionValidator createValidator,
            VoteToASuggestionValidator voteValidator,
            CommentASuggestionValidator commentValidator,
            DeleteASuggestionValidator deleteValidator,
            DeleteASuggestionCommentValidator deleteCommentValidator)
        {
            _dbContext = dbContext;
            _repository = repository;
            _mapper = mapper;
            _notificationHandler = notificationHandler;
            _createValidator = createValidator;
            _voteValidator = voteValidator;
            _commentValidator = commentValidator;
            _deleteValidator = deleteValidator;
            _deleteCommentValidator = deleteCommentValidator;
        }


        #region Write
        public Suggestion Create(CreateASuggestionCommand createASugestion)
        {
            var validationContext = new CreateASuggestionValidationContext(createASugestion, _repository);
            
            _createValidator.Validate(validationContext);

            var suggestion = _mapper.Map<Suggestion>(createASugestion);

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SaveChanges();

            var createANotificationCommand = _mapper.Map<CreateANotificationCommand>(suggestion);
            
            _notificationHandler.CreateANotification(createANotificationCommand);

            return suggestion;
        }

        public Suggestion Vote(VoteToASuggestionCommand voteToASuggestionCommand)
        {
            var validationContext = new VoteToASuggestionValidationContext(voteToASuggestionCommand, _repository);

            _voteValidator.Validate(validationContext);

            var suggestion = _repository.GetFirstSuggestion(voteToASuggestionCommand.SuggestionId);
            var suggestionVote = _repository.GetUserVoteOrDefault(suggestion, voteToASuggestionCommand.UserId);
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

            _commentValidator.Validate(validationContext);

            var suggestionComment = _mapper.Map<SuggestionComment>(commentASuggestion);
            var suggestion = _repository.AddComment(commentASuggestion.SuggestionId, suggestionComment);

            _dbContext.SaveChanges();

            var createANotificationCommand = _mapper.Map<CreateANotificationCommand>(suggestionComment);
            createANotificationCommand.ConcernedUserIds = new [] { suggestion.CreatedBy };

            _notificationHandler.CreateANotification(createANotificationCommand);

            return suggestion;
        }

        public Guid Delete(DeleteASuggestionCommand deleteASuggestion)
        {
            var validationContext = new DeleteASuggestionValidationContext(deleteASuggestion, _repository);

            _deleteValidator.Validate(validationContext);

            _repository.RemoveUserSuggestion(deleteASuggestion.Id, deleteASuggestion.UserId);
            _dbContext.SaveChanges();

            return deleteASuggestion.Id;
        }

        public Suggestion DeleteAComment(DeleteASuggestionCommentCommand deleteASuggestionComment)
        {
            var validationContext = new DeleteASuggestionCommentValidationContext(deleteASuggestionComment, _repository);

            _deleteCommentValidator.Validate(validationContext);

            var suggestion = _repository.RemoveUserComment(deleteASuggestionComment.SuggestionId, deleteASuggestionComment.Id, deleteASuggestionComment.UserId);

            _dbContext.SaveChanges();

            return suggestion;
        }
        #endregion
    }
}