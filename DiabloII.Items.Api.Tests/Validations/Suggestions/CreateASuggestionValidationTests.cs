using System;
using DiabloII.Items.Api.Domain.Commands.Suggestions;
using DiabloII.Items.Api.Domain.Exceptions;
using DiabloII.Items.Api.Domain.Models.Suggestions;
using DiabloII.Items.Api.Domain.Repositories;
using DiabloII.Items.Api.Domain.Validations.Suggestions.Create;
using DiabloII.Items.Api.Infrastructure.DbContext;
using DiabloII.Items.Api.Infrastructure.Helpers;
using DiabloII.Items.Api.Infrastructure.Repositories;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Items.Api.Tests.Validations.Suggestions
{
    [TestFixture]
    public class CreateASuggestionValidationTests
    {
        private ApplicationDbContext _dbContext;
        private CreateASuggestionValidator _validator;
        private CreateASuggestionValidationContext _validationContext;
        private ISuggestionRepository _repository;

        [SetUp]
        public void Setup()
        {
            var validCommand = new CreateASuggestionCommand
            {
                Content = "any value",
                Ip = "213.91.163.4"
            };

            _dbContext = DatabaseHelpers.CreateMyTestDbContext();
            _repository = new SuggestionRepository(_dbContext);

            _validator = new CreateASuggestionValidator();
            _validationContext = new CreateASuggestionValidationContext(validCommand, _repository);
        }

        [Test]
        public void WhenContentIsNull_ShouldThrowABadRequestException()
        {
            _validationContext.Command.Content = null;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenContentIsEmpty_ShouldThrowABadRequestException()
        {
            _validationContext.Command.Content = string.Empty;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenContentIsLongerThan500Characters_ShouldThrowABadRequestException()
        {
            _validationContext.Command.Content = new String('x', 501);

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenIpIsNull_ShouldThrowABadRequestException()
        {
            _validationContext.Command.Ip = null;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenIpIsEmpty_ShouldThrowABadRequestException()
        {
            _validationContext.Command.Ip = string.Empty;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenIpIsNotAnIpV4_ShouldThrowABadRequestException()
        {
            _validationContext.Command.Ip = "213.91.163.4444";

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenContentIsNotUnique_ShouldThrowABadRequestException()
        {
            var suggestionContent = "any value";

            _validationContext.Command.Content = suggestionContent;

            _dbContext.Suggestions.Add(new Suggestion { Id = Guid.NewGuid(), Content = suggestionContent });
            _dbContext.SaveChanges();

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenCommandIsValid_ShouldSuccess() => Should.NotThrow(() => _validator.Validate(_validationContext));
    }
}