using DiabloII.Domain.Commands.Users;
using DiabloII.Domain.Exceptions;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Validations.Users.Create;
using DiabloII.Infrastructure.Repositories;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Infrastructure.Tests.Validations.Users
{
    [TestFixture]
    public class CreateAUserValidationTests : BaseValidationTests<CreateAUserValidator, CreateAUserValidationContext>
    {
        private IUserRepository _repository;

        [SetUp]
        public void Setup()
        {
            var validCommand = new CreateAUserCommand
            {
                UserId = "any user id",
                Email = "DiabloIIEnriched@gmail.com",
            };

            _repository = new UserRepository(_dbContext);
            _validationContext = new CreateAUserValidationContext(validCommand, _repository);
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
        public void WhenEmailIsNotUnique_ShouldThrowABadRequestException() =>
            ShouldThrowDuringTheValidation<BadRequestException>(() =>
            {
                CreateTheUser();

                _validationContext.RepositoryValidationContext.Id = "other user id";
            });

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