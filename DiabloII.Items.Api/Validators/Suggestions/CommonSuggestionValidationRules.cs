using DiabloII.Items.Api.Exceptions;
using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions
{
    public static class CommonSuggestionValidationRules
    {
        public static IRuleBuilder<T, SuggestionDbContextValidationContext> SuggestionShouldExists<T>(this IRuleBuilder<T, SuggestionDbContextValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.Repository.DoesSuggestionExists(context.Id))
            .OnFailure(context => throw new NotFoundException("Suggestion"));

        public static IRuleBuilder<T, SuggestionDbContextValidationContext> SuggestionContentShouldBeUnique<T>(this IRuleBuilder<T, SuggestionDbContextValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.Repository.DoesSuggestionContentIsUnique(context.Content))
            .OnFailure(context => throw new BadRequestException("Content should be unique"));
      
        public static IRuleBuilder<T, SuggestionDbContextValidationContext> SuggestionShouldBeRelatedToTheUserIp<T>(this IRuleBuilder<T, SuggestionDbContextValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.Repository.DoesSuggestionExists(context.Id, context.Ip))
            .OnFailure(context => throw new UnauthorizedException("Suggestion"));
    }
}