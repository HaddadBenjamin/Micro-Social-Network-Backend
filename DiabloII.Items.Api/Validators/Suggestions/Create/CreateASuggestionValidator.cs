using System.Data;
using DiabloII.Items.Api.Validators.Suggestions.Vote;
using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.Create
{
    public class CreateASuggestionValidator : AbstractValidator<CreateASuggestionValidatorContext>
    {
        public CreateASuggestionValidator()
        {
            RuleFor(context => context.Dto.Content).SuggestionContentShouldNotBeNullOrEmpty();
            RuleFor(context => context.Dto.Ip)
                .SuggestionIpShouldNotBeNullOrEmpty()
                .SuggestionIpShouldBeAnIpV4();
            RuleFor(context => context).SuggestionContentShouldBeUnique();
        }
    }
}