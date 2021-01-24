using DiabloII.Domain.Commands.Domains.Notifications;

namespace DiabloII.Domain.Validations.Notifications.Create
{
    public class CreateANotificationValidationContext
    {
        public CreateANotificationCommand Command { get; set; }

        public CreateANotificationValidationContext(CreateANotificationCommand command) => Command = command;
    }
}