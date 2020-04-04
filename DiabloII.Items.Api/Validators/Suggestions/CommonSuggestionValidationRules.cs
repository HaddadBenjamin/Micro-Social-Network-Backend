using System.Linq;
using DiabloII.Items.Api.Exceptions;
using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions
{
    public static class CommonSuggestionExtensions
    {
        public static IRuleBuilder<T, SuggestionDbContextValidatorContext> SuggestionShouldExists<T>(this IRuleBuilder<T, SuggestionDbContextValidatorContext> ruleBuilder) => ruleBuilder
            .Must(context => context.DbContext.Suggestions.Any(suggestion => suggestion.Id == context.Id))
            .OnFailure(context => throw new BadRequestException("Suggestion does not exists"));

        public static IRuleBuilder<T, SuggestionDbContextValidatorContext> SuggestionContentShouldBeUnique<T>(this IRuleBuilder<T, SuggestionDbContextValidatorContext> ruleBuilder) => ruleBuilder
            .Must(context => context.DbContext.Suggestions.All(suggestion => suggestion.Content != context.Content))
            .OnFailure(context => throw new BadRequestException("Content should be unique"));

        public static IRuleBuilder<T, SuggestionDbContextValidatorContext> SuggestionShouldExistsAndBeRelatedToTheUserIp<T>(this IRuleBuilder<T, SuggestionDbContextValidatorContext> ruleBuilder) => ruleBuilder
            .Must(context => context.DbContext.Suggestions.Any(suggestion => suggestion.Id == context.Id && suggestion.Ip == context.Ip))
            .OnFailure(context => throw new BadRequestException("Suggestion does not exists or is not related to the user ip"));
    }
}