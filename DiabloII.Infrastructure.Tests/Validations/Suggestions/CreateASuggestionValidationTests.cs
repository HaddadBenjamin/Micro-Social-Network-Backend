using System;
using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Exceptions;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Validations.Suggestions.Create;
using DiabloII.Infrastructure.Repositories;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Infrastructure.Tests.Validations.Suggestions
{
    [TestFixture]
    public class CreateASuggestionValidationTests : BaseValidationTests<CreateASuggestionValidator, CreateASuggestionValidationContext>
    {
        private ISuggestionRepository _repository;

        [SetUp]
        public void Setup()
        {
            var validCommand = new CreateASuggestionCommand
            {
                Content = "any value",
                UserId = "any value"
            };

            _repository = new SuggestionRepository(_dbContext);
            _validationContext = new CreateASuggestionValidationContext(validCommand, _repository);
        }

        [Test]
        public void WhenContentIsNull_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() => _validationContext.Command.Content = null);

        [Test]
        public void WhenContentIsEmpty_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() => _validationContext.Command.Content = string.Empty);

        [Test]
        public void WhenContentIsLongerThan500Characters_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() => _validationContext.Command.Content = new String('x', 501));

        [Test]
        public void WhenUserIdIsNull_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() => _validationContext.Command.UserId = null);

        [Test]
        public void WhenUserIdIsEmpty_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() => _validationContext.Command.UserId = string.Empty);

        [Test]
        public void WhenContentIsNotUnique_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() =>
            {
                var suggestionContent = "any value";

                _validationContext.Command.Content = suggestionContent;

                _dbContext.Suggestions.Add(new Suggestion {Id = Guid.NewGuid(), Content = suggestionContent});
                _dbContext.SaveChanges();
            });

        [Test]
        public void WhenCommandIsValid_ShouldSuccess() => Should.NotThrow(() => _validator.Validate(_validationContext));
    }
}