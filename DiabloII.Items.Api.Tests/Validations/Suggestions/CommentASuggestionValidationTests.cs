using System;
using DiabloII.Items.Api.Application.Validations.Suggestions.Comment;
using DiabloII.Items.Api.Domain.Commands.Suggestions;
using DiabloII.Items.Api.Domain.Exceptions;
using DiabloII.Items.Api.Domain.Models.Suggestions;
using DiabloII.Items.Api.Infrastructure.DbContext;
using DiabloII.Items.Api.Infrastructure.Helpers;
using DiabloII.Items.Api.Infrastructure.Repositories.Suggestions;
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
            var validCommand = new CommentASuggestionCommand
            {
                SuggestionId = Guid.NewGuid(),
                Ip = "213.91.163.4",
                Comment = "any comment"
            };

            _dbContext = DatabaseHelpers.CreateMyTestDbContext();
            _repository = new SuggestionRepository(_dbContext);

            _validator = new CommentASuggestionValidator();
            _validationContext = new CommentASuggestionValidationContext(validCommand, _repository);
        }

        [Test]
        public void WhenCommentIsNull_ShouldThrowABadRequestException()
        {
            _validationContext.Command.Comment = null;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenCommentIsEmpty_ShouldThrowABadRequestException()
        {
            _validationContext.Command.Comment = string.Empty;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenCommentIsLongerThan500Characters_ShouldThrowABadRequestException()
        {
            _validationContext.Command.Comment = new String('x', 501);

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
        public void WhenSuggestionDoesNotExists_ShouldThrowANotFoundException() =>
            Should.Throw<NotFoundException>(() => _validator.Validate(_validationContext));

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
                Ip = _validationContext.Command.Ip
            };

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SaveChanges();
        }
    }
}