using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.Vote
{
    public class VoteToASuggestionValidator : AbstractValidator<VoteToASuggestionValidatorContext>
    {
        public VoteToASuggestionValidator()
        {
            RuleFor(context => context.Dto.Ip)
                .SuggestionIpShouldNotBeNullOrEmpty()
                .SuggestionIpShouldBeAnIpV4();

            RuleFor(context => context)
                .SuggestionShouldExists()
                .SuggestionShouldUniqueByIpAndId();
        }
    }
}