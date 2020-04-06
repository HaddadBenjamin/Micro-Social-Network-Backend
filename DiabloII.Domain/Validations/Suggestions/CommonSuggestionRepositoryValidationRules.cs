using DiabloII.Domain.Exceptions;
using FluentValidation;

namespace DiabloII.Domain.Validations.Suggestions
{
    public static class CommonSuggestionRepositoryValidationRules
    {
        public static IRuleBuilder<T, CommonSuggestionRepositoryValidationContext> SuggestionShouldExists<T>(this IRuleBuilder<T, CommonSuggestionRepositoryValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.Repository.DoesSuggestionExists(context.Id))
            .OnFailure(context => throw new NotFoundException("Suggestion"));

        public static IRuleBuilder<T, CommonSuggestionRepositoryValidationContext> SuggestionCommentShouldExists<T>(this IRuleBuilder<T, CommonSuggestionRepositoryValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.Repository.DoesCommentExists(context.CommentId))
            .OnFailure(context => throw new NotFoundException("Suggestion comment"));

        public static IRuleBuilder<T, CommonSuggestionRepositoryValidationContext> SuggestionContentShouldBeUnique<T>(this IRuleBuilder<T, CommonSuggestionRepositoryValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.Repository.DoesSuggestionContentIsUnique(context.Content))
            .OnFailure(context => throw new BadRequestException("Content should be unique"));
      
        public static IRuleBuilder<T, CommonSuggestionRepositoryValidationContext> ShouldBeOwnerOfTheSuggestion<T>(this IRuleBuilder<T, CommonSuggestionRepositoryValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.Repository.IsOwnerOfTheSuggestion(context.Id, context.UserId))
            .OnFailure(context => throw new UnauthorizedException("Suggestion"));

        public static IRuleBuilder<T, CommonSuggestionRepositoryValidationContext> ShouldNotBeOwnerOfTheSuggestion<T>(this IRuleBuilder<T, CommonSuggestionRepositoryValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => !context.Repository.IsOwnerOfTheSuggestion(context.Id, context.UserId))
            .OnFailure(context => throw new UnauthorizedException("Suggestion"));

    }
}