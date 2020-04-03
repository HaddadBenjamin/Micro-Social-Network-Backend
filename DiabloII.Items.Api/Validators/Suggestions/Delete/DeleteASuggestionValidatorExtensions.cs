using System.Linq;
using DiabloII.Items.Api.Exceptions;
using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.Delete
{
    public static class DeleteASuggestionValidatorExtensions
    {
        public static void SuggestionShouldExistsAndBeRelatedToTheUserIp<T>(this IRuleBuilder<T, DeleteASuggestionValidatorContext> ruleBuilder) => ruleBuilder
            .Must(context => context.DbContext.Suggestions.Any(suggestion => suggestion.Id == context.Dto.Id && suggestion.Ip == context.Dto.Ip))
            .OnFailure(context => throw new BadRequestException("Suggestion does not exists or is not related to the user ip"));
    }
}