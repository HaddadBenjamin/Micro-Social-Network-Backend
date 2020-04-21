using DiabloII.Domain.Commands.Notifications;
using DiabloII.Domain.Exceptions;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Validations.Notifications.Create;
using DiabloII.Infrastructure.DbContext;
using DiabloII.Infrastructure.Tests.Helpers;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Infrastructure.Tests.Validations.Notifications
{
    [TestFixture]
    public class CreateANotificationValidationTests
    {
        private ApplicationDbContext _dbContext;
        private CreateANotificationValidator _validator;
        private CreateANotificationValidationContext _validationContext;

        [SetUp]
        public void Setup()
        {
            var validCommand = new CreateANotificationCommand
            {
                Title = "New patch 1.623",
                Content = "A new area and some monsters have been added",
                Type = NotificationType.PatchNotes
            };

            _dbContext = DatabaseHelpers.CreateMyTestDbContext();

            _validator = new CreateANotificationValidator();
            _validationContext = new CreateANotificationValidationContext(validCommand);
        }

        [Test]
        public void WhenTitleIsNull_ShouldThrowABadRequestException()
        {
            _validationContext.Command.Title = null;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenTitleIsEmpty_ShouldThrowABadRequestException()
        {
            _validationContext.Command.Title = string.Empty;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenContentIsNull_ShouldThrowABadRequestException()
        {
            _validationContext.Command.Content = null;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenContentIsEmpty_ShouldThrowABadRequestException()
        {
            _validationContext.Command.Content = string.Empty;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenNotificationTypeIsNone_ShouldThrowABadRequestException()
        {
            _validationContext.Command.Type = NotificationType.None;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenCommandIsValid_ShouldSuccess() => Should.NotThrow(() => _validator.Validate(_validationContext));
    }
}