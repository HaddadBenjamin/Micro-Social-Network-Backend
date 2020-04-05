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

        public SuggestionCommandHandler(ApplicationDbContext dbContext, ISuggestionRepository repository, IMapper mapper)
        {
            _dbContext = dbContext;
            _repository = repository;
            _mapper = mapper;
        }


        #region Write
        public Suggestion Create(CreateASuggestionCommand createASugestion)
        {
            var validationContext = new CreateASuggestionValidationContext(createASugestion, _repository);
            var validator = new CreateASuggestionValidator();

            validator.Validate(validationContext);

            var suggestion = _mapper.Map<Suggestion>(createASugestion);

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SaveChanges();

            return suggestion;
        }

        public Suggestion Vote(VoteToASuggestionCommand voteToASuggestionCommand)
        {
            var validationContext = new VoteToASuggestionValidationContext(voteToASuggestionCommand, _repository);
            var validator = new VoteToASuggestionValidator();

            validator.Validate(validationContext);

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
            var validator = new CommentASuggestionValidator();

            validator.Validate(validationContext);

            var suggestionComment = _mapper.Map<SuggestionComment>(commentASuggestion);
            var suggestion = _repository.AddComment(commentASuggestion.SuggestionId, suggestionComment);

            _dbContext.SaveChanges();

            return suggestion;
        }

        public Guid Delete(DeleteASuggestionCommand deleteASuggestion)
        {
            var validationContext = new DeleteASuggestionValidationContext(deleteASuggestion, _repository);
            var validator = new DeleteASuggestionValidator();

            validator.Validate(validationContext);

            _repository.RemoveSuggestion(deleteASuggestion.Id, deleteASuggestion.Ip);
            _dbContext.SaveChanges();

            return deleteASuggestion.Id;
        }

        public Suggestion DeleteAComment(DeleteASuggestionCommentCommand deleteASuggestionComment)
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