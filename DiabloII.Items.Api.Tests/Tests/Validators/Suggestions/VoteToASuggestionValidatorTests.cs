using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Suggestions;
using DiabloII.Items.Api.Exceptions;
using DiabloII.Items.Api.Queries.Suggestions;
using DiabloII.Items.Api.Tests.Helpers;
using DiabloII.Items.Api.Validators.Suggestions.Vote;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Items.Api.Tests.Tests.Validators.Suggestions
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
        public void WhenUserIpIsNull_ShouldThrowABadRequestException()
        {
            ValidatorContext.Dto = new VoteToASuggestionDto { Ip = null };

            Should.Throw<BadRequestException>(() => Validator.Validate(ValidatorContext));
        }

        [Test]
        public void WhenUserIpIsEmpty_ShouldThrowABadRequestException()
        {
            ValidatorContext.Dto = new VoteToASuggestionDto { Ip = string.Empty };

            Should.Throw<BadRequestException>(() => Validator.Validate(ValidatorContext));
        }

        [Test]
        public void WhenUserIpIsNotAnIp_ShouldThrowABadRequestException()
        {
            ValidatorContext.Dto = new VoteToASuggestionDto { Ip = "999.0.0.0" };

            Should.Throw<BadRequestException>(() => Validator.Validate(ValidatorContext));
        }

        [Test]
        public void WhenSuggestionDoesNotExists_ShouldThrowABadRequestException()
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
        public void WhenSuggestionVoteIsNotUniqueByIpAndSuggestionId_ShouldThrowABadRequestException()
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