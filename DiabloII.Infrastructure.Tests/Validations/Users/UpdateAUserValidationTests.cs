using DiabloII.Domain.Commands.Users;
using DiabloII.Domain.Exceptions;
using DiabloII.Domain.Helpers;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Repositories.Domains;
using DiabloII.Domain.Validations.Users.Update;
using DiabloII.Infrastructure.Repositories;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Infrastructure.Tests.Validations.Users
{
    [TestFixture]
    public class UpdateAUserValidationTests : BaseValidationTests<UpdateAUserValidator, UpdateAUserValidationContext>
    {
        private IUserRepository _repository;

        [SetUp]
        public void Setup()
        {
            var acceptedNotifiers = new[] { NotifierType.InApp, NotifierType.Mail };
            var acceptedNotifications = new[] { NotificationType.CreatedSuggestion, NotificationType.NewCommentOnYourSuggestion };
            var validCommand = new UpdateAUserCommand
            {
                UserId = "any user id",
                Email = "DiabloIIEnriched@gmail.com",
                AcceptedNotifications = EnumerationFlagsHelpers.ToInteger(acceptedNotifications),
                AcceptedNotifiers = EnumerationFlagsHelpers.ToInteger(acceptedNotifiers)
            };

            _repository = new UserRepository(_dbContext);
            _validationContext = new UpdateAUserValidationContext(validCommand, _repository);

            CreateTheUser();
        }

        [Test]
        public void WhenUserIdIsNull_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() =>
                _validationContext.Command.UserId = null);

        [Test]
        public void WhenUserIdIsEmpty_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() =>
                _validationContext.Command.UserId = string.Empty);

        [Test]
        public void WhenEmailIsNotValid_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() =>
                _validationContext.Command.Email = "not valid email");

        [Test]
        public void WhenUserNotExists_ShouldThrowANotFoundException() =>
            ShouldThrowDuringTheValidation<NotFoundException>(() =>
                _validationContext.RepositoryValidationContext.Id = "other user id");

        [Test]
        public void WhenCommandIsValid_ShouldSuccess() => Should.NotThrow(() => _validator.Validate(_validationContext));

        private void CreateTheUser()
        {
            var user = new User
            {
                Id = _validationContext.Command.UserId,
                Email = _validationContext.Command.Email
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}