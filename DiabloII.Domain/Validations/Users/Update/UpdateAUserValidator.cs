using FluentValidation;

namespace DiabloII.Domain.Validations.Users.Update
{
    public class UpdateAUserValidator : AbstractValidator<UpdateAUserValidationContext>
    {
        public UpdateAUserValidator()
        {
            RuleFor(context => context.Command.Id).ShouldNotBeNullOrEmpty("Id");
            RuleFor(context => context.Command.Email).ShouldBeNullOrAValidEmail("Email");

            RuleFor(context => context.RepositoryValidationContext).UserShouldExists();
        }
    }
}