
namespace ExtensionMethodsConsole
{  
    public static class NumberExtensions
    {
        public static bool IsBetween(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }
    }
}