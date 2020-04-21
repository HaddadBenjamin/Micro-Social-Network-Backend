using System;
using DiabloII.Infrastructure.DbContext;
using DiabloII.Infrastructure.Tests.Helpers;
using FluentValidation;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Infrastructure.Tests.Validations
{
    [TestFixture]
    public abstract class BaseValidationTests<Validator, ValidationContext>
        where Validator : IValidator<ValidationContext>, new()
    {
        protected Validator _validator { get; private set; }

        protected ValidationContext _validationContext;

        protected ApplicationDbContext _dbContext { get; private set; }

        [SetUp]
        public void Setup()
        {
            _dbContext = DatabaseHelpers.CreateMyTestDbContext();
            _validator = new Validator();
        }

        protected void ShouldThrowDuringTheValidation<ExceptionType>(Action beforeValidationAction = null) where ExceptionType : Exception
        {
            beforeValidationAction?.Invoke();

            Should.Throw<ExceptionType>(() => _validator.Validate(_validationContext));
        }
    }
}