using System.Linq;
using DiabloII.Items.Api.Exceptions;
using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.Create
{
    public static class CreateASuggestionValidatorExtensions
    {
        public static void SuggestionContentShouldNotBeNullOrEmpty<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder
            .NotNull()
            .NotEmpty()
            .OnFailure(context => throw new BadRequestException("Content should not be null or empty"));

        public static void SuggestionContentShouldBeUnique<T>(this IRuleBuilder<T, CreateASuggestionValidatorContext> ruleBuilder) => ruleBuilder
            .Must(context => context.DbContext.Suggestions.All(suggestion => suggestion.Content != context.Dto.Content))
            .OnFailure(context => throw new BadRequestException("Content should be unique"));
    }
}