using DiabloII.Domain.Commands.Users;
using DiabloII.Domain.Exceptions;
using DiabloII.Domain.Helpers;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Validations.Users.Update;
using DiabloII.Infrastructure.DbContext;
using DiabloII.Infrastructure.Repositories;
using DiabloII.Infrastructure.Tests.Helpers;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Infrastructure.Tests.Validations.Users
{
    [TestFixture]
    public class UpdateAUserValidationTests
    {
        private ApplicationDbContext _dbContext;
        private UpdateAUserValidator _validator;
        private UpdateAUserValidationContext _validationContext;
        private IUserRepository _repository;

        [SetUp]
        public void Setup()
        {
            var acceptedNotifiers = new[] { NotifierType.InApp, NotifierType.Mail };
            var acceptedNotifications = new[] { NotificationType.CreatedSuggestion, NotificationType.NewCommentOnYourSuggestion};
            var validCommand = new UpdateAUserCommand
            {
                UserId = "any value",
                Email = "DiabloIIEnriched@gmail.com",
                AcceptedNotifications = EnumerationFlagsHelpers.ToInteger(acceptedNotifications),
                AcceptedNotifiers = EnumerationFlagsHelpers.ToInteger(acceptedNotifiers)
            };

            _dbContext = DatabaseHelpers.CreateMyTestDbContext();
            _repository = new UserRepository(_dbContext);

            _validator = new UpdateAUserValidator();
            _validationContext = new UpdateAUserValidationContext(validCommand, _repository);

            CreateTheUser();
        }

        [Test]
        public void WhenUserIdIsNull_ShouldThrowABadRequestException()
        {
            _validationContext.Command.UserId = null;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenUserIdIsEmpty_ShouldThrowABadRequestException()
        {
            _validationContext.Command.UserId = string.Empty;

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenEmailIsNotValid_ShouldThrowABadRequestException()
        {
            _validationContext.Command.Email = "not valid email";

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenUserNotExists_ShouldThrowANotFoundException()
        {
            _validationContext.RepositoryValidationContext.Id = "other user id";

            Should.Throw<NotFoundException>(() => _validator.Validate(_validationContext));
        }

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