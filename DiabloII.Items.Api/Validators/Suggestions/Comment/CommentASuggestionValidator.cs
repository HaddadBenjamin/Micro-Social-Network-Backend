﻿using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.Comment
{
    public class CommentASuggestionValidator : AbstractValidator<CommentASuggestionValidatorContext>
    {
        public CommentASuggestionValidator()
        {
            RuleFor(context => context.Dto.Comment)
                .ShouldNotBeNullOrEmpty("Comment")
                .ShouldBeShorterThan("Comment");
            RuleFor(context => context.Dto.Ip).ShouldBeAValidIp();
            RuleFor(context => context.DbContextValidatorContext).SuggestionShouldExists();
        }
    }
}