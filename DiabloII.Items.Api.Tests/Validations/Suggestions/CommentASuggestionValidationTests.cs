using System;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Suggestions.Models;
using DiabloII.Items.Api.Exceptions;
using DiabloII.Items.Api.Helpers;
using DiabloII.Items.Api.Repositories.Suggestions;
using DiabloII.Items.Api.Requests.Suggestions;
using DiabloII.Items.Api.Validations.Suggestions.Comment;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Items.Api.Tests.Validations.Suggestions
{
    [TestFixture]
    public class CommentASuggestionValidationTests
    {
        private ApplicationDbContext _dbContext;
        private CommentASuggestionValidator _validator;
        private CommentASuggestionValidationContext _validationContext;
        private ISuggestionRepository _repository;

        [SetUp]
        public void Setup()
        {
            var validDto = new CommentASuggestionDto
            {
                SuggestionId = Guid.NewGuid(),
                Ip = "213.91.163.4",
                Comment = "any comment"
            };

            _dbContext = DatabaseHelpers.CreateMyTestDbContext();
            _repository = new SuggestionRepository(_dbContext);

            _validator = new CommentASuggestionValidator();
            _validationContext = new CommentASuggestionValidationContext(validDto, _repository);
        }

        [Test]
        public void WhenCommentIsNull_ShouldThrowABadRequestException()
        {
            _validationContext.Dto.Comment = null;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenCommentIsEmpty_ShouldThrowABadRequestException()
        {
            _validationContext.Dto.Comment = string.Empty;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenCommentIsLongerThan500Characters_ShouldThrowABadRequestException()
        {
            _validationContext.Dto.Comment = new String('x', 501);

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
        public void WhenSuggestionDoesNotExists_ShouldThrowANotFoundException() =>
            Should.Throw<NotFoundException>(() => _validator.Validate(_validationContext));

        [Test]
        public void WhenDtoIsValid_ShouldSuccess()
        {
            AddTheValidSuggestion();

            Should.NotThrow(() => _validator.Validate(_validationContext));
        }

        private void AddTheValidSuggestion()
        {
            var suggestion = new Suggestion
            {
                Id = _validationContext.Dto.SuggestionId,
                Ip = _validationContext.Dto.Ip
            };

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SaveChanges();
        }
    }
}