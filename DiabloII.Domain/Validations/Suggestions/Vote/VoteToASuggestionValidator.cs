using FluentValidation;

namespace DiabloII.Domain.Validations.Suggestions.Vote
{
    public class VoteToASuggestionValidator : AbstractValidator<VoteToASuggestionValidationContext>
    {
        public VoteToASuggestionValidator()
        {
            RuleFor(context => context.Command.UserId).ShouldNotBeNullOrEmpty("UserId");
            RuleFor(context => context.RepositoryValidationContext)
                .SuggestionShouldExists()
                .ShouldNotBeOwnerOfTheSuggestion();
        }
    }
}