using System.Linq;
using DiabloII.Items.Api.Exceptions;
using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions
{
    public static class CommonSuggestionValidationRules
    {
        public static IRuleBuilder<T, SuggestionDbContextValidatorContext> SuggestionShouldExists<T>(this IRuleBuilder<T, SuggestionDbContextValidatorContext> ruleBuilder) => ruleBuilder
            .Must(context => context.DbContext.Suggestions.Any(suggestion => suggestion.Id == context.Id))
            .OnFailure(context => throw new NotFoundException("Suggestion"));

        public static IRuleBuilder<T, SuggestionDbContextValidatorContext> SuggestionContentShouldBeUnique<T>(this IRuleBuilder<T, SuggestionDbContextValidatorContext> ruleBuilder) => ruleBuilder
            .Must(context => context.DbContext.Suggestions.All(suggestion => suggestion.Content != context.Content))
            .OnFailure(context => throw new BadRequestException("Content should be unique"));

        public static IRuleBuilder<T, SuggestionDbContextValidatorContext> SuggestionShouldBeRelatedToTheUserIp<T>(this IRuleBuilder<T, SuggestionDbContextValidatorContext> ruleBuilder) => ruleBuilder
            .Must(context => context.DbContext.Suggestions.Any(suggestion => suggestion.Id == context.Id && suggestion.Ip == context.Ip))
            .OnFailure(context => throw new UnauthorizedException("Suggestion"));
    }
}