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
        public static void ShouldBeEquals<TCompareType>(this Table expectedTable, TCompareType actual, bool ignoreNotSpecifiedMembers = true)
        {
            var compareLogic = CreateTheCompareLogic<TCompareType>(expectedTable, ignoreNotSpecifiedMembers);
            var differences = GetTheDifferences(expectedTable, actual, compareLogic);

            if (differences.Any())
            {
                var exceptionMessage = GetTheDifferencesMessage<TCompareType>(differences);

                throw new NotEqualsException(exceptionMessage);
            }
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
            
            return new CompareLogic()
            {
                Config = new ComparisonConfig
                {
                    IgnoreCollectionOrder = true,
                    MembersToIgnore = ignoreNotSpecifiedMembers ? membersToIgnore : new List<string>()
                }
            };
        }

        private static IEnumerable<dynamic> GetTheDifferences<TCompareType>(Table expectedTable, TCompareType actual, CompareLogic compareLogic)
        {
            var expected = expectedTable.CreateInstance<TCompareType>();
            var compareResult = compareLogic.Compare(expected, actual);
            
            return compareResult.Differences.Select(difference =>
            {
                dynamic jObject = new JObject();

                jObject.PropertyName = difference.PropertyName;
                jObject.Actual = difference.Object2Value;
                jObject.Expected = difference.Object1Value;

                return jObject.ToString();
            });
        }

        private static string GetTheDifferencesMessage<TCompareType>(IEnumerable<dynamic> differences) =>
            $"Type : {typeof(TCompareType)}{Environment.NewLine}Differences Count : {differences.Count()}{Environment.NewLine}Differences : {string.Join(Environment.NewLine, differences)}";
    }
}