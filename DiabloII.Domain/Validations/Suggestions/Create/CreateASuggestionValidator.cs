using FluentValidation;

namespace DiabloII.Domain.Validations.Suggestions.Create
{
    public class CreateASuggestionValidator : AbstractValidator<CreateASuggestionValidationContext>
    {
        public CreateASuggestionValidator()
        {
            RuleFor(context => context.Command.UserId).ShouldNotBeNullOrEmpty("UserId"); 
            
            RuleFor(context => context.Command.Content)
                .ShouldNotBeNullOrEmpty("Content")
                .ShouldBeShorterThan("Content");
           
            RuleFor(context => context.RepositoryValidationContext).SuggestionContentShouldBeUnique();
        }
    }
}