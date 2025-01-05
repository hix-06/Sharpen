
namespace ExtensionMethodsConsole
{
    public static class StringExtensions
    {
        public static string RemoveWhiteSpaces(this string value)
        {
            return new string(value.Replace(" ", ""));
        }

        public static string Reverse(this string value)
        {
            char[] charArray = value.ToCharArray();
            charArray.Reverse();
            return new string(charArray);
        }
    }
}