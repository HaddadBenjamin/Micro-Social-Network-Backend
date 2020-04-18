using DiabloII.Domain.Exceptions;
using DiabloII.Domain.Models.Notifications;
using FluentValidation;

namespace DiabloII.Domain.Validations.Notifications
{
    public static class CommonNotificationValidationRules
    {
        public static void ShouldNotBeNoneNotificationType<T>(this IRuleBuilder<T, NotificationType> ruleBuilder) => ruleBuilder
            .Must(notificationType => notificationType != NotificationType.None)
            .OnFailure(context => throw new BadRequestException("Notification type should not be none"));
    }
}