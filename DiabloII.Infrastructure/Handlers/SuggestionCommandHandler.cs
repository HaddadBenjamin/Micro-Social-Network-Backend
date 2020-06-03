using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DiabloII.Domain.Commands.Domains.Notifications;
using DiabloII.Domain.Commands.Domains.Suggestions;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Repositories.Domains;
using DiabloII.Domain.Validations.Suggestions.Comment;
using DiabloII.Domain.Validations.Suggestions.Create;
using DiabloII.Domain.Validations.Suggestions.Delete;
using DiabloII.Domain.Validations.Suggestions.DeleteAComment;
using DiabloII.Domain.Validations.Suggestions.Vote;
using DiabloII.Infrastructure.DbContext;
using MediatR;

namespace DiabloII.Infrastructure.Handlers
{
    public class SuggestionCommandHandler :
        IRequestHandler<CreateASuggestionCommand>,
        IRequestHandler<DeleteASuggestionCommand>,
        IRequestHandler<DeleteASuggestionCommentCommand>,
        IRequestHandler<VoteToASuggestionCommand>,
        IRequestHandler<CommentASuggestionCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ISuggestionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly CreateASuggestionValidator _createValidator;
        private readonly VoteToASuggestionValidator _voteValidator;
        private readonly CommentASuggestionValidator _commentValidator;
        private readonly DeleteASuggestionValidator _deleteValidator;
        private readonly DeleteASuggestionCommentValidator _deleteCommentValidator;

        public SuggestionCommandHandler(
            ISuggestionRepository repository,
            IMapper mapper,
            IMediator mediator,
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
            _mediator = mediator;
            _createValidator = createValidator;
            _voteValidator = voteValidator;
            _commentValidator = commentValidator;
            _deleteValidator = deleteValidator;
            _deleteCommentValidator = deleteCommentValidator;
        }


        public async Task<Unit> Handle(CreateASuggestionCommand createASugestion, CancellationToken cancellationToken = default)
        {
            var validationContext = new CreateASuggestionValidationContext(createASugestion, _repository);
            _createValidator.Validate(validationContext);

            var suggestion = _mapper.Map<Suggestion>(createASugestion);
            _dbContext.Suggestions.Add(suggestion);
            await _dbContext.SaveChangesAsync();

            var createANotificationCommand = _mapper.Map<CreateANotificationCommand>(suggestion);
            await _mediator.Send(createANotificationCommand);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteASuggestionCommand deleteASuggestion, CancellationToken cancellationToken = default)
        {
            var validationContext = new DeleteASuggestionValidationContext(deleteASuggestion, _repository);
            _deleteValidator.Validate(validationContext);

            _repository.RemoveUserSuggestion(deleteASuggestion.Id, deleteASuggestion.UserId);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteASuggestionCommentCommand deleteASuggestionComment, CancellationToken cancellationToken = default)
        {
            var validationContext = new DeleteASuggestionCommentValidationContext(deleteASuggestionComment, _repository);
            _deleteCommentValidator.Validate(validationContext);

            _repository.RemoveUserComment(deleteASuggestionComment.SuggestionId, deleteASuggestionComment.Id, deleteASuggestionComment.UserId);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        public async Task<Unit> Handle(VoteToASuggestionCommand voteToASuggestionCommand, CancellationToken cancellationToken = default)
        {
            var validationContext = new VoteToASuggestionValidationContext(voteToASuggestionCommand, _repository);
            _voteValidator.Validate(validationContext);

            var suggestion = _repository.Get(voteToASuggestionCommand.SuggestionId);
            var suggestionVote = _repository.GetUserVoteOrDefault(suggestion, voteToASuggestionCommand.UserId);
            var suggestionVoteExists = suggestionVote != null;

            if (suggestionVoteExists)
                _repository.RemoveVote(suggestion, suggestionVote);
            else
            {
                suggestionVote = _mapper.Map<SuggestionVote>(voteToASuggestionCommand);
                _repository.AddVote(suggestion, suggestionVote);
            }

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        public async Task<Unit> Handle(CommentASuggestionCommand commentASuggestion, CancellationToken cancellationToken = default)
        {
            var validationContext = new CommentASuggestionValidationContext(commentASuggestion, _repository);
            _commentValidator.Validate(validationContext);

            var suggestionComment = _mapper.Map<SuggestionComment>(commentASuggestion);
            _repository.AddComment(commentASuggestion.SuggestionId, suggestionComment);

            var suggestion = _repository.Get(commentASuggestion.SuggestionId);
            suggestionComment.Suggestion = suggestion;

            await _dbContext.SaveChangesAsync();

            var createANotificationCommand = _mapper.Map<CreateANotificationCommand>(suggestionComment);
            createANotificationCommand.ConcernedUserIds = new[] { suggestion.CreatedBy };

            await _mediator.Send(createANotificationCommand);

            return Unit.Value;
        }
    }
}