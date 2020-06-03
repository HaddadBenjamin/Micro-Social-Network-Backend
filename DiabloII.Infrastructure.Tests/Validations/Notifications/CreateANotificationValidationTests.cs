using DiabloII.Domain.Commands.Domains.Notifications;
using DiabloII.Domain.Exceptions;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Validations.Notifications.Create;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Infrastructure.Tests.Validations.Notifications
{
    [TestFixture]
    public class CreateANotificationValidationTests : BaseValidationTests<CreateANotificationValidator, CreateANotificationValidationContext>
    {
        [SetUp]
        public void Setup()
        {
            var validCommand = new CreateANotificationCommand
            {
                Title = "New patch 1.623",
                Content = "A new area and some monsters have been added",
                Type = NotificationType.PatchNotes
            };

            _validationContext = new CreateANotificationValidationContext(validCommand);
        }

        [Test]
        public void WhenTitleIsNull_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() => _validationContext.Command.Title = null);

        [Test]
        public void WhenTitleIsEmpty_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() => _validationContext.Command.Title = string.Empty);

        [Test]
        public void WhenContentIsNull_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() => _validationContext.Command.Content = null);

        [Test]
        public void WhenContentIsEmpty_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() => _validationContext.Command.Content = string.Empty);

        [Test]
        public void WhenNotificationTypeIsNone_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() => _validationContext.Command.Type = NotificationType.None);

        [Test]
        public void WhenCommandIsValid_ShouldSuccess() => Should.NotThrow(() => _validator.Validate(_validationContext));
    }
}