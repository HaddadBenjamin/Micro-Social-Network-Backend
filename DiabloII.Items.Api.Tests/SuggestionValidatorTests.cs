using System;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Suggestions;
using DiabloII.Items.Api.Exceptions;
using DiabloII.Items.Api.Queries.Suggestions;
using DiabloII.Items.Api.Services.Suggestions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Items.Api.Tests
{
    [TestFixture]
    public class SuggestionValidatorTests
    {
        private ApplicationDbContext DbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;

            DbContext =  new ApplicationDbContext(options);
        }

        [Test]
        public void ValidateCreateSuggestionDto_ShouldThrowABadRequestException_WhenContentIsNull()
        {
            var createSuggestionDto = new CreateSuggestionDto { Content = null };

            Should.Throw<BadRequestException>(() => SuggestionValidator.Validate(createSuggestionDto, DbContext));
        }

        [Test]
        public void ValidateCreateSuggestionDto_ShouldThrowABadRequestException_WhenContentIsEmpty()
        {
            var createSuggestionDto = new CreateSuggestionDto { Content = string.Empty };

            Should.Throw<BadRequestException>(() => SuggestionValidator.Validate(createSuggestionDto, DbContext));
        }

        [Test]
        public void ValidateCreateSuggestionDto_ShouldThrowABadRequestException_WhenContentIsNotUnique()
        {
            var suggestionContent = "any value";
            var createSuggestionDto = new CreateSuggestionDto { Content = suggestionContent };
           
            DbContext.Suggestions.Add(new Suggestion { Id = 1, Content = suggestionContent });
            DbContext.SaveChanges();
            
            Should.Throw<BadRequestException>(() => SuggestionValidator.Validate(createSuggestionDto, DbContext));
        }

        [Test]
        public void ValidateSuggestionVoteDto_ShouldThrowABadRequestException_WhenUserIpIsNull()
        {
            var suggestionVoteDto = new SuggestionVoteDto { Ip = null };
           
            Should.Throw<BadRequestException>(() => SuggestionValidator.Validate(suggestionVoteDto, DbContext));
        }

        [Test]
        public void ValidateSuggestionVoteDto_ShouldThrowABadRequestException_WhenUserIpIsEmpty()
        {
            var suggestionVoteDto = new SuggestionVoteDto { Ip = string.Empty };

            Should.Throw<BadRequestException>(() => SuggestionValidator.Validate(suggestionVoteDto, DbContext));
        }

        [Test]
        public void ValidateSuggestionVoteDto_ShouldThrowABadRequestException_WhenUserIpIsNotAnIp()
        {
            var suggestionVoteDto = new SuggestionVoteDto { Ip = "not an Ip" };

            Should.Throw<BadRequestException>(() => SuggestionValidator.Validate(suggestionVoteDto, DbContext));
        }

        [Test]
        public void ValidateSuggestionVoteDto_ShouldThrowABadRequestException_WhenSuggestionDoesNotExists()
        {
            var suggestionIdThatDontExists = int.MaxValue;
            var suggestionVoteDto = new SuggestionVoteDto
            {
                Ip = "193.43.55.67",
                SuggestionId = suggestionIdThatDontExists
            };

            Should.Throw<BadRequestException>(() => SuggestionValidator.Validate(suggestionVoteDto, DbContext));
        }

        [Test]
        public void ValidateSuggestionVoteDto_ShouldThrowABadRequestException_WhenSuggestionVoteIsNotUniqueByIpAndSuggestionId()
        {
            var userIp = "193.43.55.67";
            var suggestionVoteDto = new SuggestionVoteDto
            {
                Ip = userIp,
                SuggestionId = 1
            };
            
            DbContext.Suggestions.Add(new Suggestion { Id = 1, Content = "any content" });
            DbContext.SuggestionVotes.Add(new SuggestionVote { Id = 2, SuggestionId = 1, Ip = userIp });
            DbContext.SaveChanges();

            Should.Throw<BadRequestException>(() => SuggestionValidator.Validate(suggestionVoteDto, DbContext));
        }
    }
}
