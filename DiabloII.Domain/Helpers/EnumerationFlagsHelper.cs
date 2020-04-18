using System;
using System.Collections.Generic;
using System.Linq;

namespace DiabloII.Domain.Helpers
{
    public static class EnumerationFlagsHelper
    {
        public static int ToInteger<EnumerationType>(IEnumerable<EnumerationType> enumerations) where EnumerationType : struct, IConvertible => enumerations
            .Select(enumeration => (int)(object)enumeration)
            .Aggregate((flagsSum, flagToAdd) => flagsSum | flagToAdd);

        public static IEnumerable<string> ToStrings<EnumerationType>(int enumerationValue)
            where EnumerationType : struct, IConvertible =>
            Enum.GetValues(typeof(EnumerationType))
                .Cast<EnumerationType>()
                .Where(enumeration => (enumerationValue & (int) (object) enumeration) != 0)
                .Select(enumeration => enumeration.ToString());
    }
}