using System;
using System.Collections.Generic;
using System.Linq;
using DiabloII.Application.Tests.Exceptions;
using KellermanSoftware.CompareNetObjects;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Extensions
{
    public static class TableExtensions
    {
        public static void ShouldBeEqualsTo<TCompareType>(this Table expectedTable, TCompareType actual, Func<TableRow, TCompareType> mapper = null, bool ignoreNotSpecifiedMembers = true)
        {
            var compareLogic = CreateTheCompareLogic<TCompareType>(expectedTable, ignoreNotSpecifiedMembers);
            var expected = mapper == null ? expectedTable.CreateInstance<TCompareType>() : mapper(expectedTable.Rows.First());
            var differences = GetTheDifferences(actual, expected, compareLogic);

            HandleTheDifferences<TCompareType>(differences);
        }

        public static void ShouldAllExistsIn<TCompareType>(this Table expectedTable, IReadOnlyCollection<TCompareType> actuals, bool ignoreNotSpecifiedMembers = true)
        {
            var compareLogic = CreateTheCompareLogic<TCompareType>(expectedTable, ignoreNotSpecifiedMembers);
            var allExpected = expectedTable.CreateSet<TCompareType>();

            var differences = allExpected
                .SelectMany(expected => GetTheLeastDifferences(actuals, expected, compareLogic))
                .ToList();

            HandleTheDifferences<TCompareType>(differences);
        }

        public static void ShouldExistsIn<TCompareType>(this Table expectedTable, IReadOnlyCollection<TCompareType> actuals, bool ignoreNotSpecifiedMembers = true)
        {
            var compareLogic = CreateTheCompareLogic<TCompareType>(expectedTable, ignoreNotSpecifiedMembers);
            var expected = expectedTable.CreateInstance<TCompareType>();
            var differences = GetTheLeastDifferences(actuals, expected, compareLogic);

            HandleTheDifferences<TCompareType>(differences);
        }

        private static CompareLogic CreateTheCompareLogic<TCompareType>(Table expectedTable, bool ignoreNotSpecifiedMembers = true)
        {
            var tableHeaders = expectedTable.Header;
            var clastMemberNames = typeof(TCompareType)
                .GetMembers()
                .Select(field => field.Name)
                .ToList();
            var membersToIgnore = clastMemberNames
                .Except(tableHeaders)
                .ToList();
            
            return new CompareLogic
            {
                Config = new ComparisonConfig
                {
                    IgnoreCollectionOrder = true,
                    MaxDifferences =  int.MaxValue,
                    MembersToIgnore = ignoreNotSpecifiedMembers ? membersToIgnore : new List<string>()
                }
            };
        }

        private static IReadOnlyCollection<dynamic> GetTheLeastDifferences<TCompareType>(IReadOnlyCollection<TCompareType> actuals, TCompareType expected, CompareLogic compareLogic) => actuals
            .Select(actual => GetTheDifferences(actual, expected, compareLogic))
            .OrderBy(actual => actual.Count())
            .FirstOrDefault()
            .ToList();

        private static IEnumerable<dynamic> GetTheDifferences<TCompareType>(TCompareType actual, TCompareType expected, CompareLogic compareLogic)
        {
            var compareResult = compareLogic.Compare(expected, actual);

            return compareResult.Differences
                .Where(difference => difference.PropertyName != string.Empty)
                .Select(difference =>
                {
                    dynamic jObject = new JObject();

                    jObject.PropertyName = difference.PropertyName;
                    jObject.Actual = difference.Object2Value;
                    jObject.Expected = difference.Object1Value;

                    return jObject.ToString();
                });
        }

        private static void HandleTheDifferences<TCompareType>(IEnumerable<dynamic> differences)
        {
            if (differences.Any())
            {
                var exceptionMessage = GetTheDifferencesMessage<TCompareType>(differences);

                throw new NotEqualsException(exceptionMessage);
            }
        }

        private static string GetTheDifferencesMessage<TCompareType>(IEnumerable<dynamic> differences) =>
            $"{Environment.NewLine}Type : {typeof(TCompareType)}{Environment.NewLine}Differences Count : {differences.Count()}{Environment.NewLine}Differences : {string.Join(Environment.NewLine, differences)}";
    }
}