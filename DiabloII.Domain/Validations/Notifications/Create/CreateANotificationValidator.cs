using FluentValidation;

namespace DiabloII.Domain.Validations.Notifications.Create
{
    public class CreateANotificationValidator : AbstractValidator<CreateANotificationValidationContext>
    {
        public CreateANotificationValidator()
        {
            RuleFor(context => context.Command.Title).ShouldNotBeNullOrEmpty("Title");
            RuleFor(context => context.Command.Content).ShouldNotBeNullOrEmpty("Content");

            RuleFor(context => context.Command.Type).ShouldNotBeNoneNotificationType();
        }
    }
}