using System;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Suggestions.Models;
using DiabloII.Items.Api.Exceptions;
using DiabloII.Items.Api.Helpers;
using DiabloII.Items.Api.Repositories.Suggestions;
using DiabloII.Items.Api.Requests.Suggestions;
using DiabloII.Items.Api.Validators.Suggestions.Create;
using NUnit.Framework;
using Shouldly;
using FluentValidation.TestHelper;
using NUnit.Framework.Internal;

namespace DiabloII.Items.Api.Tests.Validators.Suggestions
{
    [TestFixture]
    public class CreateASuggestionValidatorTests
    {
        private ApplicationDbContext _dbContext;
        private CreateASuggestionValidator _validator;
        private CreateASuggestionValidationContext _validationContext;
        private ISuggestionRepository _repository;

        [SetUp]
        public void Setup()
        {
            var validDto = new CreateASuggestionDto
            {
                Content = "any value",
                Ip = "213.91.163.4"
            };

            _dbContext = DatabaseHelpers.CreateMyTestDbContext();
            _repository = new SuggestionRepository(_dbContext);

            _validator = new CreateASuggestionValidator();
            _validationContext = new CreateASuggestionValidationContext(validDto, _repository);
        }

        [Test]
        public void WhenContentIsNull_ShouldThrowABadRequestException()
        {
            _validationContext.Dto.Content = null;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenContentIsEmpty_ShouldThrowABadRequestException()
        {
            _validationContext.Dto.Content = string.Empty;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenContentIsLongerThan500Characters_ShouldThrowABadRequestException()
        {
            _validationContext.Dto.Content = new String('x', 1000);

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenIpIsNull_ShouldThrowABadRequestException()
        {
            _validationContext.Dto.Ip = null;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenIpIsEmpty_ShouldThrowABadRequestException()
        {
            _validationContext.Dto.Ip = string.Empty;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenIpIsNotAnIpV4_ShouldThrowABadRequestException()
        {
            _validationContext.Dto.Ip = "213.91.163.4444";

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenContentIsNotUnique_ShouldThrowABadRequestException()
        {
            var suggestionContent = "any value";

            _validationContext.Dto.Content = suggestionContent;

            _dbContext.Suggestions.Add(new Suggestion { Id = Guid.NewGuid(), Content = suggestionContent });
            _dbContext.SaveChanges();

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }
    }
}