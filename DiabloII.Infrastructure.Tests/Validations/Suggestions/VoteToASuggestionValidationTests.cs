using System;
using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Exceptions;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Validations.Suggestions.Vote;
using DiabloII.Infrastructure.DbContext;
using DiabloII.Infrastructure.Repositories;
using DiabloII.Infrastructure.Tests.Helpers;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Infrastructure.Tests.Validations.Suggestions
{
    [TestFixture]
    public class VoteToASuggestionValidationTests
    {
        private ApplicationDbContext _dbContext;
        private VoteToASuggestionValidator _validator;
        private VoteToASuggestionValidationContext _validationContext;
        private ISuggestionRepository _repository;

        [SetUp]
        public void Setup()
        {
            var validCommand = new VoteToASuggestionCommand
            {
                Ip = "213.91.163.4",
                IsPositive = true,
                SuggestionId = Guid.NewGuid()
            };

            _dbContext = DatabaseHelpers.CreateMyTestDbContext();
            _repository = new SuggestionRepository(_dbContext);
          
            _validator = new VoteToASuggestionValidator();
            _validationContext = new VoteToASuggestionValidationContext(validCommand, _repository);
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
            var suggestion = new Suggestion
            {
                Id = _validationContext.Command.SuggestionId
            };

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SaveChanges();

            Should.NotThrow(() => _validator.Validate(_validationContext));
        }
    }
}