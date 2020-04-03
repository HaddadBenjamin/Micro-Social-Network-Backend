using System.Linq;
using System.Text.RegularExpressions;
using DiabloII.Items.Api.Exceptions;
using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.Vote
{
    public static class VoteToASuggestionValidatorExtensions
    {
        private static readonly string IpV4Regex = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";
       
        public static IRuleBuilder<T, string> SuggestionIpShouldNotBeNullOrEmpty<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder
            .NotNull()
            .NotEmpty()
            .OnFailure(context => throw new BadRequestException("Ip should not be null or empty"));

        public static void SuggestionIpShouldBeAnIpV4<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder
            .Must(ip => Regex.Match(ip, IpV4Regex).Success)
            .OnFailure(context => throw new BadRequestException("Ip should be an IPV4"));

        public static IRuleBuilder<T, VoteToASuggestionValidatorContext> SuggestionShouldExists<T>(this IRuleBuilder<T, VoteToASuggestionValidatorContext> ruleBuilder) => ruleBuilder
            .Must(context => context.DbContext.Suggestions.Any(suggestion => suggestion.Id == context.Dto.SuggestionId))
            .OnFailure(context => throw new BadRequestException("Suggestion does not exists"));

        public static void SuggestionShouldUniqueByIpAndId<T>(this IRuleBuilder<T, VoteToASuggestionValidatorContext> ruleBuilder) => ruleBuilder
            .Must(context => !context.DbContext.SuggestionVotes.Any(suggestionVote =>
                suggestionVote.Suggestion.Id == context.Dto.SuggestionId &&
                suggestionVote.Ip == context.Dto.Ip))
            .OnFailure(context => throw new BadRequestException("Suggestion is not unique"));
    }
}