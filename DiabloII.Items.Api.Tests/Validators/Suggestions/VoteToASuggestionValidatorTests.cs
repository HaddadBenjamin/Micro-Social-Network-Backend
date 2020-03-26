using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Suggestions;
using DiabloII.Items.Api.Exceptions;
using DiabloII.Items.Api.Helpers;
using DiabloII.Items.Api.Queries.Suggestions;
using DiabloII.Items.Api.Validators.Suggestions.Vote;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Items.Api.Tests.Validators.Suggestions
{
    [TestFixture]
    public class VoteToASuggestionValidatorTests
    {
        private ApplicationDbContext _dbContext;
        private VoteToASuggestionValidator _validator;
        private VoteToASuggestionValidatorContext _validatorContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = DatabaseHelpers.CreateATestDatabase();

            _validator = new VoteToASuggestionValidator();
            _validatorContext = new VoteToASuggestionValidatorContext(null, _dbContext);
        }

        [Test]
        public void WhenUserIpIsNull_ShouldThrowABadRequestException()
        {
            _validatorContext.Dto = new VoteToASuggestionDto { Ip = null };

            Should.Throw<BadRequestException>(() => _validator.Validate(_validatorContext));
        }

        [Test]
        public void WhenUserIpIsEmpty_ShouldThrowABadRequestException()
        {
            _validatorContext.Dto = new VoteToASuggestionDto { Ip = string.Empty };

            Should.Throw<BadRequestException>(() => _validator.Validate(_validatorContext));
        }

        [Test]
        public void WhenUserIpIsNotAnIp_ShouldThrowABadRequestException()
        {
            _validatorContext.Dto = new VoteToASuggestionDto { Ip = "999.0.0.0" };

            Should.Throw<BadRequestException>(() => _validator.Validate(_validatorContext));
        }

        [Test]
        public void WhenSuggestionDoesNotExists_ShouldThrowABadRequestException()
        {
            var suggestionIdThatDontExists = int.MaxValue;

            _validatorContext.Dto = new VoteToASuggestionDto
            {
                Ip = "193.43.55.67",
                SuggestionId = suggestionIdThatDontExists
            };

            Should.Throw<BadRequestException>(() => _validator.Validate(_validatorContext));
        }

        [Test]
        public void WhenSuggestionVoteIsNotUniqueByIpAndSuggestionId_ShouldThrowABadRequestException()
        {
            var userIp = "193.43.55.67";

            _validatorContext.Dto = new VoteToASuggestionDto

            {
                Ip = userIp,
                SuggestionId = 1
            };

            _dbContext.Suggestions.Add(new Suggestion { Id = 1, Content = "any content" });
            _dbContext.SuggestionVotes.Add(new SuggestionVote { Id = 2, SuggestionId = 1, Ip = userIp });
            _dbContext.SaveChanges();

            Should.Throw<BadRequestException>(() => _validator.Validate(_validatorContext));
        }
    }
}