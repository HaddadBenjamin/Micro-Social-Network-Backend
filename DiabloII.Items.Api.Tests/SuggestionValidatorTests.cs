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
        public void ValidateCreateSuggestionDto_ShouldThrowABadRequestException_OnNullContent()
        {
            var createSuggestionDto = new CreateSuggestionDto { Content = null };

            Should.Throw<BadRequestException>(() => SuggestionValidator.Validate(createSuggestionDto, DbContext));
        }

        [Test]
        public void ValidateCreateSuggestionDto_ShouldThrowABadRequestException_OnEmptyContent()
        {
            var createSuggestionDto = new CreateSuggestionDto { Content = string.Empty };

            Should.Throw<BadRequestException>(() => SuggestionValidator.Validate(createSuggestionDto, DbContext));
        }

        [Test]
        public void ValidateCreateSuggestionDto_ShouldThrowABadRequestException_OnNotUniqueContent()
        {
            var suggestionContent = "any value";
            var createSuggestionDto = new CreateSuggestionDto { Content = suggestionContent };
           
            DbContext.Suggestions.Add(new Suggestion { Id = 1, Content = suggestionContent });
            DbContext.SaveChanges();
            
            Should.Throw<BadRequestException>(() => SuggestionValidator.Validate(createSuggestionDto, DbContext));
        }
    }
}


//public static void Validate(CreateSuggestionDto createSugestion, ApplicationDbContext dbContext)
//{
//var suggestionContentIsUnique = dbContext.Suggestions.Any(suggestion => suggestion.Content == createSugestion.Content);

//    if (suggestionContentIsUnique)
//throw new BadRequestException(nameof(createSugestion.Content), "should be unique");
//}

//public static void Validate(SuggestionVoteDto suggestionVoteDto, ApplicationDbContext dbContext)
//{
//var userIpEmpty = string.IsNullOrWhiteSpace(suggestionVoteDto.Ip);

//    if (userIpEmpty)
//throw new BadRequestException(nameof(suggestionVoteDto.Ip), "should not be empty");

//var userIpIsValid = Regex.Match(suggestionVoteDto.Ip, IpV4Regex).Success;

//    if (!userIpIsValid)
//throw new BadRequestException(nameof(suggestionVoteDto.Ip), "should be an IpV4");

//var suggestionExists = dbContext.Suggestions.Any(suggestion => suggestion.Id == suggestionVoteDto.SuggestionId);

//    if (!suggestionExists)
//throw new BadRequestException(nameof(suggestionVoteDto.SuggestionId), "not exists");

//var suggestionVoteIsUniqueByIpAndSuggestionId = dbContext.SuggestionVotes.Any(suggestionVote =>
//        suggestionVote.SuggestionId == suggestionVoteDto.SuggestionId &&
//        suggestionVote.Ip == suggestionVoteDto.Ip);

//    if (suggestionVoteIsUniqueByIpAndSuggestionId)
//throw new BadRequestException($"{nameof(suggestionVoteDto.SuggestionId)} and {nameof(suggestionVoteDto.Ip)}", "should be unique together");
//}
//}