using DiabloII.Items.Api.Domain.Exceptions;
using FluentValidation;

namespace DiabloII.Items.Api.Application.Validations.Suggestions.DeleteAComment
{
    public static class DeleteASuggestionCommentValidationRules
    {
        public static void SuggestionAndCommentShouldExistsAndBeRelatedToTheUserIp<T>(this IRuleBuilder<T, DeleteASuggestionCommentValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.Repository.DoesUserCommentExists(context.Command.SuggestionId, context.Command.Id, context.Command.Ip))
            .OnFailure(context => throw new UnauthorizedException("Suggestion comment"));
    }
}