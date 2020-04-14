using DiabloII.Domain.Exceptions;
using FluentValidation;

namespace DiabloII.Domain.Validations
{
    public static class CommonValidationRules
    {
        public static IRuleBuilder<T, string> ShouldNotBeNullOrEmpty<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName) => ruleBuilder
            .NotNull()
            .NotEmpty()
            .OnFailure(context => throw new BadRequestException($"{fieldName} should not be null or empty"));

        public static IRuleBuilder<T, string> ShouldBeShorterThan<T>(this IRuleBuilder<T,string> ruleBuilder, string fieldName, int maxLength = 500) => ruleBuilder
            .MaximumLength(maxLength)
            .OnFailure(context => throw new BadRequestException($"{fieldName} should be shorter than {maxLength} characters"));
    }
}