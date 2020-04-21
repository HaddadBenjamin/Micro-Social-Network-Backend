using DiabloII.Domain.Commands.Notifications;

namespace DiabloII.Domain.Validations.Notifications.Create
{
    public class CreateANotificationValidationContext
    {
        public CreateANotificationCommand Command { get; set; }

        public CreateANotificationValidationContext(CreateANotificationCommand command) => Command = command;
    }
}