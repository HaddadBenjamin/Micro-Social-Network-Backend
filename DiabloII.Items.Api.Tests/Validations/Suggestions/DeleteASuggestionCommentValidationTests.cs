using System;
using System.Collections.Generic;
using System.Linq;
using DiabloII.Items.Api.Application.Validations.Suggestions.DeleteAComment;
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
    public class DeleteASuggestionCommentValidationTests
    {
        private ApplicationDbContext _dbContext;
        private DeleteASuggestionCommentValidator _validator;
        private DeleteASuggestionCommentValidationContext _validationContext;
        private ISuggestionRepository _repository;

        [SetUp]
        public void Setup()
        {
            var validCommand = new DeleteASuggestionCommentCommand
            {
                Id = Guid.NewGuid(),
                SuggestionId = Guid.NewGuid(),
                Ip = "213.91.163.4"
            };

            _dbContext = DatabaseHelpers.CreateMyTestDbContext();
            _repository = new SuggestionRepository(_dbContext);

            _validator = new DeleteASuggestionCommentValidator();
            _validationContext = new DeleteASuggestionCommentValidationContext(validCommand, _repository);
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
        public void WhenSuggestionCommentDoesNotExists_ShouldThrowANotFoundException()
        {
            AddTheValidSuggestion();

            var suggestion = _dbContext.Suggestions.First();
            var comments = suggestion.Comments;

            comments = comments.Select(comment => new SuggestionComment {Id = Guid.NewGuid()}).ToList();
            suggestion.Comments = comments;

            _dbContext.SaveChanges();

            Should.Throw<NotFoundException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenUserIsNotOwnerOfTheSuggestionComment_ShouldThrowAUnauthorizedException()
        {
            AddTheValidSuggestion();

            var suggestion = _dbContext.Suggestions.First();
            var comment = suggestion.Comments.First();
            var newComment = new SuggestionComment
            {
                Id = comment.Id,
                Ip = "213.91.163.1"
            };

            _repository.RemoveComment(suggestion.Id, comment.Id, comment.Ip);
            _repository.AddComment(suggestion.Id, newComment);
            _dbContext.SaveChanges();

            Should.Throw<UnauthorizedException>(() => _validator.Validate(_validationContext));
        }

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
                Ip = _validationContext.Command.Ip,
                Comments = new List<SuggestionComment>
                {
                    new SuggestionComment
                    {
                        Id = _validationContext.Command.Id,
                        Ip = _validationContext.Command.Ip
                    }
                }
            };

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SaveChanges();
        }
    }
}