using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.Vote
{
    public class VoteToASuggestionValidator : AbstractValidator<VoteToASuggestionValidatorContext>
    {
        public VoteToASuggestionValidator()
        {
            RuleFor(context => context.Dto.Ip).ShouldBeAValidIp();
            RuleFor(context => context.DbContextValidatorContext).SuggestionShouldExists();
        }
    }
}