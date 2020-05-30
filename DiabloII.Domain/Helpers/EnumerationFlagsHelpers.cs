using System;
using System.Collections.Generic;
using System.Linq;

namespace DiabloII.Domain.Helpers
{
    public static class EnumerationFlagsHelpers
    {
        public static int ToInteger<EnumerationType>(IEnumerable<EnumerationType> enumerations) where EnumerationType : struct, IConvertible
        {
            var enumerationValues = enumerations
                .Select(enumeration => (int)(object)enumeration)
                .ToList();

            return enumerationValues.Any()
                ? enumerationValues.Aggregate((flagsSum, flagToAdd) => flagsSum | flagToAdd)
                : 0;
        }

        public static IEnumerable<string> ToStrings<EnumerationType>(int enumerationValue)
            where EnumerationType : struct, IConvertible =>
            Enum.GetValues(typeof(EnumerationType))
                .Cast<EnumerationType>()
                .Where(enumeration => (enumerationValue & (int)(object)enumeration) != 0)
                .Select(enumeration => enumeration.ToString());
    }
}