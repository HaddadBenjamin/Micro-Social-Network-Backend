using System;
using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Exceptions;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Repositories.Domains;
using DiabloII.Domain.Validations.Suggestions.Comment;
using DiabloII.Infrastructure.Repositories;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Infrastructure.Tests.Validations.Suggestions
{
    [TestFixture]
    public class CommentASuggestionValidationTests : BaseValidationTests<CommentASuggestionValidator, CommentASuggestionValidationContext>
    {
        private ISuggestionRepository _repository;

        [SetUp]
        public void Setup()
        {
            var validCommand = new CommentASuggestionCommand
            {
                SuggestionId = Guid.NewGuid(),
                UserId = "valid user id",
                Comment = "any value"
            };

            _repository = new SuggestionRepository(_dbContext);
            _validationContext = new CommentASuggestionValidationContext(validCommand, _repository);
        }

        [Test]
        public void WhenCommentIsNull_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() => _validationContext.Command.Comment = null);

        [Test]
        public void WhenCommentIsEmpty_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() => _validationContext.Command.Comment = string.Empty);

        [Test]
        public void WhenCommentIsLongerThan500Characters_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() => _validationContext.Command.Comment = new String('x', 501));

        [Test]
        public void WhenUserIdIsNull_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() => _validationContext.Command.UserId = null);

        [Test]
        public void WhenUserIdIsEmpty_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() => _validationContext.Command.UserId = string.Empty);

        [Test]
        public void WhenSuggestionDoesNotExists_ShouldThrowANotFoundException() =>
            ShouldThrowDuringTheValidation<NotFoundException>();

        [Test]
        public void WhenCommandIsValid_ShouldSuccess()
        {
            AddTheValidSuggestion();

            Should.NotThrow(() => _validator.Validate(_validationContext));
        }

        private void AddTheValidSuggestion()
        {
            var suggestion = new Suggestion
            {
                Id = _validationContext.Command.SuggestionId,
                CreatedBy = _validationContext.RepositoryValidationContext.UserId
            };

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SaveChanges();
        }
    }
}