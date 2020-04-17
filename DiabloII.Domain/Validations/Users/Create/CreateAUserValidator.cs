using FluentValidation;

namespace DiabloII.Domain.Validations.Users.Create
{
    public class CreateAUserValidator : AbstractValidator<CreateAUserValidationContext>
    {
        public CreateAUserValidator()
        {
            RuleFor(context => context.Command.UserId).ShouldNotBeNullOrEmpty("UserId");

            RuleFor(context => context.RepositoryValidationContext).UserShouldNotExists();
        }
    }
}