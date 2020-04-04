using System;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Suggestions.Models;
using DiabloII.Items.Api.Exceptions;
using DiabloII.Items.Api.Helpers;
using DiabloII.Items.Api.Repositories.Suggestions;
using DiabloII.Items.Api.Requests.Suggestions;
using DiabloII.Items.Api.Vallidations.Suggestions.Delete;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Items.Api.Tests.Validations.Suggestions
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
            var validDto = new DeleteASuggestionDto
            {
                Ip = "213.91.163.4",
                Id = Guid.NewGuid()
            };

            _dbContext = DatabaseHelpers.CreateMyTestDbContext();
            _repository = new SuggestionRepository(_dbContext);

            _validator = new DeleteASuggestionValidator();
            _validationContext = new DeleteASuggestionValidationContext(validDto, _repository);
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
        public void WhenUserIsNotTheOwnerOsTheSuggestion_ShouldThrowAUnauthorizedException()
        {
            _validationContext.Dto.Ip = "213.91.163.2";

            AddTheValidSuggestion();
          
            Should.Throw<UnauthorizedException>(() => _validator.Validate(_validationContext));
        }

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
                Id = _validationContext.Dto.Id,
                Ip = _validationContext.Dto.Ip
            };

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SaveChanges();
        }
    }
}