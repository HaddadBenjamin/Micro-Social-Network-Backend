namespace DiabloII.Items.Reader.Extensions
{
    public static class StringExtension
    {
        public static int ParseIntOrDefault(this string text) => int.TryParse(text, out _) ? int.Parse(text) : 0;

        public static string FirstCharToUpper(this string text) => string.IsNullOrEmpty(text) ? text : char.ToUpper(text[0]) + text.Substring(1);
    }
}