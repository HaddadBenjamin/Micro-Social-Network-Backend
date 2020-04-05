﻿using DiabloII.Domain.Exceptions;
using FluentValidation;

namespace DiabloII.Domain.Validations.Suggestions
{
    public static class CommonSuggestionValidationRules
    {
        public static IRuleBuilder<T, SuggestionDbContextValidationContext> SuggestionShouldExists<T>(this IRuleBuilder<T, SuggestionDbContextValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.Repository.DoesSuggestionExists(context.Id))
            .OnFailure(context => throw new NotFoundException("Suggestion"));

        public static IRuleBuilder<T, SuggestionDbContextValidationContext> SuggestionCommentShouldExists<T>(this IRuleBuilder<T, SuggestionDbContextValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.Repository.DoesCommentExists(context.CommentId))
            .OnFailure(context => throw new NotFoundException("Suggestion comment"));

        public static IRuleBuilder<T, SuggestionDbContextValidationContext> SuggestionContentShouldBeUnique<T>(this IRuleBuilder<T, SuggestionDbContextValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.Repository.DoesSuggestionContentIsUnique(context.Content))
            .OnFailure(context => throw new BadRequestException("Content should be unique"));
      
        public static IRuleBuilder<T, SuggestionDbContextValidationContext> SuggestionShouldBeRelatedToTheUserIp<T>(this IRuleBuilder<T, SuggestionDbContextValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.Repository.DoesSuggestionExists(context.Id, context.Ip))
            .OnFailure(context => throw new UnauthorizedException("Suggestion"));
    }
}