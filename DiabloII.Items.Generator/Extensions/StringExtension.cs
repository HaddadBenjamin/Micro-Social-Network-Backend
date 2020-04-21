using System.Globalization;

namespace DiabloII.Items.Generator.Extensions
{
    public static class StringExtension
    {
        public static double ParseDoubleOrDefault(this string text) => double.TryParse(text, out _) ? double.Parse(text) : 0;

        public static int ParseIntOrDefault(this string text) => int.TryParse(text, out _) ? int.Parse(text) : 0;

        public static string FirstCharToUpper(this string text) => string.IsNullOrEmpty(text) ? text : char.ToUpper(text[0]) + text.Substring(1);

        public static string ToTitleCase(this string text) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);

        public static string ReplaceIfEquals(this string text, string equals, string replace) => text == equals ? replace : text;
    }
}