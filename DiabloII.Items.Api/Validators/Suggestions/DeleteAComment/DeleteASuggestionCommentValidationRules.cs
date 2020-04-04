using System.Linq;
using DiabloII.Items.Api.Exceptions;
using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.DeleteAComment
{
    public static class DeleteASuggestionCommentValidationRules
    {
        public static void SuggestionAndCommentShouldExistsAndBeRelatedToTheUserIp<T>(this IRuleBuilder<T, DeleteASuggestionCommentValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.DbContext
                //ISuggestionRepository.GetUserComment(suggestionId, userId, commentId)
                .GetSuggestions()
                .Any(suggestion => suggestion.Id == context.Dto.SuggestionId && 
                                   suggestion.Comments.Any(comment => comment.Id == context.Dto.Id && 
                                                                      comment.Ip == context.Dto.Ip)))
            .OnFailure(context => throw new UnauthorizedException("Suggestion comment"));
    }
}