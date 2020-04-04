using System;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Suggestions.Models;
using DiabloII.Items.Api.Exceptions;
using DiabloII.Items.Api.Helpers;
using DiabloII.Items.Api.Repositories.Suggestions;
using DiabloII.Items.Api.Requests.Suggestions;
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
        private VoteToASuggestionValidationContext _validationContext;
        private ISuggestionRepository _repository;

        [SetUp]
        public void Setup()
        {
            _dbContext = DatabaseHelpers.CreateMyTestDbContext();
            _repository = new SuggestionRepository(_dbContext);
          
            _validator = new VoteToASuggestionValidator();
            _validationContext = new VoteToASuggestionValidationContext(null, _repository);
        }

        [Test]
        public void WhenUserIpIsNull_ShouldThrowABadRequestException()
        {
            _validationContext.Dto = new VoteToASuggestionDto { Ip = null };

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenUserIpIsEmpty_ShouldThrowABadRequestException()
        {
            _validationContext.Dto = new VoteToASuggestionDto { Ip = string.Empty };

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenUserIpIsNotAnIp_ShouldThrowABadRequestException()
        {
            _validationContext.Dto = new VoteToASuggestionDto { Ip = "999.0.0.0" };

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenSuggestionDoesNotExists_ShouldThrowABadRequestException()
        {
            var suggestionIdThatDontExists = Guid.NewGuid();

            _validationContext.Dto = new VoteToASuggestionDto
            {
                Ip = "193.43.55.67",
                SuggestionId = suggestionIdThatDontExists
            };

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenSuggestionVoteIsNotUniqueByIpAndSuggestionId_ShouldThrowABadRequestException()
        {
            var suggestionId = Guid.NewGuid();
            var userIp = "193.43.55.67";

            _validationContext.Dto = new VoteToASuggestionDto
            {
                Ip = userIp,
                SuggestionId = suggestionId
            };
            var suggestion = new Suggestion {Id = suggestionId, Content = "any content"};
            var vote = new SuggestionVote {Id = Guid.NewGuid(), Ip = userIp};

            suggestion.Votes.Add(vote);

            _dbContext.Suggestions.Add(suggestion);
            _dbContext.SuggestionVotes.Add(vote);
            _dbContext.SaveChanges();

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }
    }
}