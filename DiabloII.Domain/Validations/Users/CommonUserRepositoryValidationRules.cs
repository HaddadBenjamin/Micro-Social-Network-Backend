using DiabloII.Domain.Exceptions;
using FluentValidation;

namespace DiabloII.Domain.Validations.Users
{
    public static class CommonUserRepositoryValidationRules
    {
        public static IRuleBuilder<T, CommonUserRepositoryValidationContext> UserShouldNotExists<T>(this IRuleBuilder<T, CommonUserRepositoryValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => !context.Repository.DoesUserExists(context.Id))
            .OnFailure(context => throw new AlreadyExistsException("User"));

        public static IRuleBuilder<T, CommonUserRepositoryValidationContext> UserShouldExists<T>(this IRuleBuilder<T, CommonUserRepositoryValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => context.Repository.DoesUserExists(context.Id))
            .OnFailure(context => throw new NotFoundException("User"));

        public static void EmailShouldBeNullOrUnique<T>(this IRuleBuilder<T, CommonUserRepositoryValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => string.IsNullOrEmpty(context.Email) || context.Repository.DoesEmailIsUnique(context.Email))
            .OnFailure(context => throw new BadRequestException("Email should be null or unique"));
    }
}