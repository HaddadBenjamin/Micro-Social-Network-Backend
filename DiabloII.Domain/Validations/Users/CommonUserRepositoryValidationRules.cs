﻿using DiabloII.Domain.Exceptions;
using FluentValidation;

namespace DiabloII.Domain.Validations.Users
{
    public static class CommonUserRepositoryValidationRules
    {
        public static IRuleBuilder<T, CommonUserRepositoryValidationContext> UserShouldNotExists<T>(this IRuleBuilder<T, CommonUserRepositoryValidationContext> ruleBuilder) => ruleBuilder
            .Must(context => !context.Repository.DoesUserExists(context.Id))
            .OnFailure(context => throw new AlreadyExistsException("User"));
    }
}