using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Suggestions;
using DiabloII.Items.Api.Exceptions;
using DiabloII.Items.Api.Queries.Suggestions;
using DiabloII.Items.Api.Tests.Helpers;
using DiabloII.Items.Api.Validators.Suggestions;
using DiabloII.Items.Api.Validators.Suggestions.Create;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Items.Api.Tests.Tests.Validators
{
    [TestFixture]
    public class CreateASuggestionValidatorTests
    {
        private ApplicationDbContext DbContext;
        private CreateASuggestionValidator Validator;
        private CreateASuggestionValidatorContext ValidatorContext;

        [SetUp]
        public void Setup()
        {
            DbContext = DatabaseHelper.CreateATestDatabase();

            Validator = new CreateASuggestionValidator();
            ValidatorContext = new CreateASuggestionValidatorContext(null, DbContext);
        }

        [Test]
        public void ShouldThrowABadRequestException_WhenContentIsNull()
        {
            ValidatorContext.Dto = new CreateASuggestionDto { Content = null };

            Should.Throw<BadRequestException>(() => Validator.Validate(ValidatorContext));
        }

        [Test]
        public void ValidateCreateSuggestionDto_ShouldThrowABadRequestException_WhenContentIsEmpty()
        {
            ValidatorContext.Dto = new CreateASuggestionDto { Content = string.Empty };

            Should.Throw<BadRequestException>(() => Validator.Validate(ValidatorContext));
        }

        [Test]
        public void ValidateCreateSuggestionDto_ShouldThrowABadRequestException_WhenContentIsNotUnique()
        {
            var suggestionContent = "any value";

            ValidatorContext.Dto = new CreateASuggestionDto { Content = suggestionContent };

            DbContext.Suggestions.Add(new Suggestion { Id = 1, Content = suggestionContent });
            DbContext.SaveChanges();

            Should.Throw<BadRequestException>(() => Validator.Validate(ValidatorContext));
        }
    }
}