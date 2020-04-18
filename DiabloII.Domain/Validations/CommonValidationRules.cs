using DiabloII.Domain.Exceptions;
using DiabloII.Domain.Extensions;
using FluentValidation;

namespace DiabloII.Domain.Validations
{
    public static class CommonValidationRules
    {
        public static IRuleBuilder<T, string> ShouldNotBeNullOrEmpty<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName) => ruleBuilder
            .NotNull()
            .NotEmpty()
            .OnFailure(context => throw new BadRequestException($"{fieldName} should not be null or empty"));

        public static void ShouldBeShorterThan<T>(this IRuleBuilder<T,string> ruleBuilder, string fieldName, int maxLength = 500) => ruleBuilder
            .MaximumLength(maxLength)
            .OnFailure(context => throw new BadRequestException($"{fieldName} should be shorter than {maxLength} characters"));

        public static IRuleBuilder<T, string> ShouldBeNullOrAValidEmail<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName) => ruleBuilder
            .Must(email => string.IsNullOrEmpty(email) || email.IAsValidEmail())
            .OnFailure(context => throw new BadRequestException($"{fieldName} should be a valid email or null or empty"));
    }
}