namespace ExtensionMethodsConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int percentage = -12;
            string name = "A hmed";

            if (percentage.IsBetween(0, 100))
                System.Console.WriteLine("Valid");
            else
                System.Console.WriteLine("Not valid!");

            name.RemoveWhiteSpaces();
            name.Reverse();
            name.Reverse();
            System.Console.WriteLine(name);
        }
    }
}