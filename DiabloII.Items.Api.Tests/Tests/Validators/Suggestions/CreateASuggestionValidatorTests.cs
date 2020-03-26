using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Suggestions;
using DiabloII.Items.Api.Exceptions;
using DiabloII.Items.Api.Queries.Suggestions;
using DiabloII.Items.Api.Tests.Helpers;
using DiabloII.Items.Api.Validators.Suggestions;
using DiabloII.Items.Api.Validators.Suggestions.Create;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Items.Api.Tests.Tests.Validators.Suggestions
{
    [TestFixture]
    public class CreateASuggestionValidatorTests
    {
        private ApplicationDbContext _dbContext;
        private CreateASuggestionValidator _validator;
        private CreateASuggestionValidatorContext _validatorContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = DatabaseHelpers.CreateATestDatabase();

            _validator = new CreateASuggestionValidator();
            _validatorContext = new CreateASuggestionValidatorContext(null, _dbContext);
        }

        [Test]
        public void WhenContentIsNull_ShouldThrowABadRequestException()
        {
            _validatorContext.Dto = new CreateASuggestionDto { Content = null };

            Should.Throw<BadRequestException>(() => _validator.Validate(_validatorContext));
        }

        [Test]
        public void WhenContentIsEmpty_ShouldThrowABadRequestException()
        {
            _validatorContext.Dto = new CreateASuggestionDto { Content = string.Empty };

            Should.Throw<BadRequestException>(() => _validator.Validate(_validatorContext));
        }

        [Test]
        public void WhenContentIsNotUnique_ShouldThrowABadRequestException()
        {
            var suggestionContent = "any value";

            _validatorContext.Dto = new CreateASuggestionDto { Content = suggestionContent };

            _dbContext.Suggestions.Add(new Suggestion { Id = 1, Content = suggestionContent });
            _dbContext.SaveChanges();

            Should.Throw<BadRequestException>(() => _validator.Validate(_validatorContext));
        }
    }
}