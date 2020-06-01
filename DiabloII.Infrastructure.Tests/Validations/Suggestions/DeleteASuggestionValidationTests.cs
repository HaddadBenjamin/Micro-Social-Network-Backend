using System;
using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Exceptions;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Repositories.Domains;
using DiabloII.Domain.Validations.Suggestions.Delete;
using DiabloII.Infrastructure.Repositories;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Infrastructure.Tests.Validations.Suggestions
{
    [TestFixture]
    public class DeleteASuggestionValidationTests : BaseValidationTests<DeleteASuggestionValidator, DeleteASuggestionValidationContext>
    {
        private ISuggestionRepository _repository;

        [SetUp]
        public void Setup()
        {
            var validCommand = new DeleteASuggestionCommand
            {
                UserId = "any value",
                Id = Guid.NewGuid()
            };

            _repository = new SuggestionRepository(_dbContext);
            _validationContext = new DeleteASuggestionValidationContext(validCommand, _repository);
        }

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
        public void WhenUserIsNotTheOwnerOsTheSuggestion_ShouldThrowAUnauthorizedException() =>
            ShouldThrowDuringTheValidation<UnauthorizedException>(() =>
            {
                _validationContext.Command.UserId = "other user id";

                AddTheValidSuggestion();
            });

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
                Id = _validationContext.Command.Id,
                CreatedBy = _validationContext.Command.UserId
            };

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SaveChanges();
        }
    }
}