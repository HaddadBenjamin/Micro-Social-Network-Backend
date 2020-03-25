using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Suggestions;
using DiabloII.Items.Api.Exceptions;
using DiabloII.Items.Api.Queries.Suggestions;
using DiabloII.Items.Api.Tests.Helpers;
using DiabloII.Items.Api.Validators.Suggestions.Vote;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Items.Api.Tests.Tests.Validators
{
    [TestFixture]
    public class VoteToASuggestionValidatorTests
    {
        private ApplicationDbContext DbContext;
        private VoteToASuggestionValidator Validator;
        private VoteToASuggestionValidatorContext ValidatorContext;

        [SetUp]
        public void Setup()
        {
            DbContext = DatabaseHelper.CreateATestDatabase();

            Validator = new VoteToASuggestionValidator();
            ValidatorContext = new VoteToASuggestionValidatorContext(null, DbContext);
        }

        [Test]
        public void ShouldThrowABadRequestException_WhenUserIpIsNull()
        {
            ValidatorContext.Dto = new VoteToASuggestionDto { Ip = null };

            Should.Throw<BadRequestException>(() => Validator.Validate(ValidatorContext));
        }

        [Test]
        public void ShouldThrowABadRequestException_WhenUserIpIsEmpty()
        {
            ValidatorContext.Dto = new VoteToASuggestionDto { Ip = string.Empty };

            Should.Throw<BadRequestException>(() => Validator.Validate(ValidatorContext));
        }

        [Test]
        public void ShouldThrowABadRequestException_WhenUserIpIsNotAnIp()
        {
            ValidatorContext.Dto = new VoteToASuggestionDto { Ip = "not an Ip" };

            Should.Throw<BadRequestException>(() => Validator.Validate(ValidatorContext));
        }

        [Test]
        public void ShouldThrowABadRequestException_WhenSuggestionDoesNotExists()
        {
            var suggestionIdThatDontExists = int.MaxValue;

            ValidatorContext.Dto = new VoteToASuggestionDto
            {
                Ip = "193.43.55.67",
                SuggestionId = suggestionIdThatDontExists
            };

            Should.Throw<BadRequestException>(() => Validator.Validate(ValidatorContext));
        }

        [Test]
        public void ShouldThrowABadRequestException_WhenSuggestionVoteIsNotUniqueByIpAndSuggestionId()
        {
            var userIp = "193.43.55.67";

            ValidatorContext.Dto = new VoteToASuggestionDto

            {
                Ip = userIp,
                SuggestionId = 1
            };

            DbContext.Suggestions.Add(new Suggestion { Id = 1, Content = "any content" });
            DbContext.SuggestionVotes.Add(new SuggestionVote { Id = 2, SuggestionId = 1, Ip = userIp });
            DbContext.SaveChanges();

            Should.Throw<BadRequestException>(() => Validator.Validate(ValidatorContext));
        }
    }
}