using System.Linq;
using System.Text.RegularExpressions;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Exceptions;
using DiabloII.Items.Api.Queries.Suggestions;

namespace DiabloII.Items.Api.Services.Suggestions
{
    public static class SuggestionValidator
    {
        private static readonly string IpV4Regex = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";

        public static void Validate(CreateSuggestionDto createSugestion, ApplicationDbContext dbContext)
        {
            if (string.IsNullOrWhiteSpace(createSugestion.Content))
                throw new BadRequestException(nameof(createSugestion.Content), "should not be empty");

            var suggestionContentIsUnique = dbContext.Suggestions.Any(suggestion => suggestion.Content == createSugestion.Content);

            if (!suggestionContentIsUnique)
                throw new BadRequestException(nameof(createSugestion.Content), "should be unique");
        }

        public static void Validate(SuggestionVoteDto suggestionVoteDto, ApplicationDbContext dbContext)
        {
            var userIpEmpty = string.IsNullOrWhiteSpace(suggestionVoteDto.Ip);

            if (userIpEmpty)
                throw new BadRequestException(nameof(suggestionVoteDto.Ip), "should not be empty");

            var userIpIsValid = Regex.Match(suggestionVoteDto.Ip, IpV4Regex).Success;
            
            if (!userIpIsValid)
                throw new BadRequestException(nameof(suggestionVoteDto.Ip), "should be an IpV4");

            var suggestionExists = dbContext.Suggestions.Any(suggestion => suggestion.Id == suggestionVoteDto.SuggestionId);

            if (!suggestionExists)
                throw new BadRequestException(nameof(suggestionVoteDto.SuggestionId), "not exists");

            var suggestionVoteIsUniqueByIpAndSuggestionId = dbContext.SuggestionVotes.Any(suggestionVote =>
                suggestionVote.SuggestionId == suggestionVoteDto.SuggestionId &&
                suggestionVote.Ip == suggestionVoteDto.Ip);

            if (!suggestionVoteIsUniqueByIpAndSuggestionId)
                throw new BadRequestException($"{nameof(suggestionVoteDto.SuggestionId)} and {nameof(suggestionVoteDto.Ip)}", "should be unique together");
        }
    }
}