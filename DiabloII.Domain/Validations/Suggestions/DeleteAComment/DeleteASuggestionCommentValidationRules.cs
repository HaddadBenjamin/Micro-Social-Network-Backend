using DiabloII.Domain.Exceptions;
using FluentValidation;

namespace DiabloII.Domain.Validations.Suggestions.DeleteAComment
{
    public static class DeleteASuggestionCommentValidationRules
    {
        public static void ShouldBeOwnerOfTheComment<T>(this IRuleBuilder<T, DeleteASuggestionCommentValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.Repository.IsOwnerOfTheComment(context.Command.SuggestionId, context.Command.Id, context.Command.UserId))
            .OnFailure(context => throw new UnauthorizedException("Suggestion comment"));
    }
}