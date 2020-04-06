using System;
using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Exceptions;
using DiabloII.Domain.Helpers;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Validations.Suggestions.Delete;
using DiabloII.Infrastructure.DbContext;
using DiabloII.Infrastructure.Repositories;
using DiabloII.Infrastructure.Tests.Helpers;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Infrastructure.Tests.Validations.Suggestions
{
    [TestFixture]
    public class DeleteASuggestionValidationTests
    {
        private ApplicationDbContext _dbContext;
        private DeleteASuggestionValidator _validator;
        private DeleteASuggestionValidationContext _validationContext;
        private ISuggestionRepository _repository;

        [SetUp]
        public void Setup()
        {
            var validCommand = new DeleteASuggestionCommand
            {
                UserId = "213.91.163.4",
                Id = Guid.NewGuid()
            };

            _dbContext = DatabaseHelpers.CreateMyTestDbContext();
            _repository = new SuggestionRepository(_dbContext);

            _validator = new DeleteASuggestionValidator();
            _validationContext = new DeleteASuggestionValidationContext(validCommand, _repository);
        }

        [Test]
        public void WhenIpIsNull_ShouldThrowABadRequestException()
        {
            _validationContext.Command.UserId = null;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenIpIsEmpty_ShouldThrowABadRequestException()
        {
            _validationContext.Command.UserId = string.Empty;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenIpIsNotAnIpV4_ShouldThrowABadRequestException()
        {
            _validationContext.Command.UserId = "213.91.163.4444";

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenSuggestionDoesNotExists_ShouldThrowANotFoundException() =>
            Should.Throw<NotFoundException>(() => _validator.Validate(_validationContext));

        [Test]
        public void WhenUserIsNotTheOwnerOsTheSuggestion_ShouldThrowAUnauthorizedException()
        {
            _validationContext.Command.UserId = "213.91.163.2";

            AddTheValidSuggestion();
          
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
                Id = _validationContext.Command.Id,
                CreatedBy = _validationContext.Command.UserId
            };

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SaveChanges();
        }
    }
}