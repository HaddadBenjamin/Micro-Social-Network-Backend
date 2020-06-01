using System;
using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Exceptions;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Repositories.Domains;
using DiabloII.Domain.Validations.Suggestions.Vote;
using DiabloII.Infrastructure.Repositories;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Infrastructure.Tests.Validations.Suggestions
{
    [TestFixture]
    public class VoteToASuggestionValidationTests : BaseValidationTests<VoteToASuggestionValidator, VoteToASuggestionValidationContext>
    {
        private ISuggestionRepository _repository;
        private static readonly string _suggestionUserId = "any user id";

        [SetUp]
        public void Setup()
        {
            var validCommand = new VoteToASuggestionCommand
            {
                UserId = "any value",
                IsPositive = true,
                SuggestionId = Guid.NewGuid()
            };

            _repository = new SuggestionRepository(_dbContext);
            _validationContext = new VoteToASuggestionValidationContext(validCommand, _repository);
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
        public void WhenUserIsOwnerOfTheSuggestion_ShouldThrowAUnauthorizedException() =>
            ShouldThrowDuringTheValidation<UnauthorizedException>(() =>
            {
                _validationContext.RepositoryValidationContext.UserId = _suggestionUserId;

                AddTheSuggestion();
            });

        [Test]
        public void WhenCommandIsValid_ShouldSuccess()
        {
            AddTheSuggestion();

            Should.NotThrow(() => _validator.Validate(_validationContext));
        }

        private void AddTheSuggestion()
        {
            var suggestion = new Suggestion
            {
                Id = _validationContext.Command.SuggestionId,
                CreatedBy = _suggestionUserId
            };

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SaveChanges();
        }
    }
}