using DiabloII.Domain.Commands.Users;
using DiabloII.Domain.Exceptions;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Validations.Users.Create;
using DiabloII.Infrastructure.DbContext;
using DiabloII.Infrastructure.Repositories;
using DiabloII.Infrastructure.Tests.Helpers;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Infrastructure.Tests.Validations.Users
{
    [TestFixture]
    public class CreateAUserValidationTests
    {
        private ApplicationDbContext _dbContext;
        private CreateAUserValidator _validator;
        private CreateAUserValidationContext _validationContext;
        private IUserRepository _repository;

        [SetUp]
        public void Setup()
        {
            var validCommand = new CreateAUserCommand
            {
                UserId = "any value",
                Email = "DiabloIIEnriched@gmail.com",
            };

            _dbContext = DatabaseHelpers.CreateMyTestDbContext();
            _repository = new UserRepository(_dbContext);

            _validator = new CreateAUserValidator();
            _validationContext = new CreateAUserValidationContext(validCommand, _repository);
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
        public void WhenEmailIsNotUnique_ShouldThrowABadRequestException()
        {
            AddTheUser();

            _validationContext.RepositoryValidationContext.Id = "other user id";

            Should.Throw<BadRequestException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenUserIsNotUnique_ShouldThrowAAlreadyExistsException()
        {
            AddTheUser();

            Should.Throw<AlreadyExistsException>(() => _validator.Validate(_validationContext));
        }

        [Test]
        public void WhenCommandIsValid_ShouldSuccess() => Should.NotThrow(() => _validator.Validate(_validationContext));

        private void AddTheUser()
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