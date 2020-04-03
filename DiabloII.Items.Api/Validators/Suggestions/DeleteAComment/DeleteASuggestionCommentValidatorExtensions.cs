using System.Linq;
using DiabloII.Items.Api.Exceptions;
using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.DeleteAComment
{
    public static class DeleteASuggestionCommentValidatorExtensions
    {
        public static void SuggestionAndCommentShouldExistsAndBeRelatedToTheUserIp<T>(this IRuleBuilder<T, DeleteASuggestionCommentValidatorContext> ruleBuilder) => ruleBuilder
            .Must(context => context.DbContext
                .GetSuggestions()
                .Any(suggestion => suggestion.Id == context.Dto.SuggestionId && 
                                   suggestion.Comments.Any(comment => comment.Id == context.Dto.Id && 
                                                                      comment.Ip == context.Dto.Ip)))
            .OnFailure(context => throw new BadRequestException("Suggestion or comment does not exists or is not related to the user ip"));
    }
}