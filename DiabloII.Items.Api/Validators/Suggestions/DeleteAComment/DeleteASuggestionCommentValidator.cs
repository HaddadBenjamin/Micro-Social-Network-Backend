﻿using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.DeleteAComment
{
    public class DeleteASuggestionCommentValidator : AbstractValidator<DeleteASuggestionCommentValidationContext>
    {
        public DeleteASuggestionCommentValidator()
        {
            RuleFor(context => context.Dto.Ip).ShouldBeAValidIp();
            RuleFor(context => context.DbContextValidationContext).SuggestionShouldExists();
            RuleFor(context => context).SuggestionAndCommentShouldExistsAndBeRelatedToTheUserIp();
        }
    }
}