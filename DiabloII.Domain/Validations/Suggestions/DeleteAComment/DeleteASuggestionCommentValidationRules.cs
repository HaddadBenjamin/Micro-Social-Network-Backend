using DiabloII.Domain.Exceptions;
using FluentValidation;

namespace DiabloII.Domain.Validations.Suggestions.DeleteAComment
{
    public static class DeleteASuggestionCommentValidationRules
    {
        public static void SuggestionAndCommentShouldExistsAndBeRelatedToTheUserIp<T>(this IRuleBuilder<T, DeleteASuggestionCommentValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.Repository.DoesUserCommentExists(context.Command.SuggestionId, context.Command.Id, context.Command.UserId))
            .OnFailure(context => throw new UnauthorizedException("Suggestion comment"));
    }
}