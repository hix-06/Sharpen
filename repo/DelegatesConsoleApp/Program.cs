namespace DelegatesConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
            1. Anonymous Methods
            An anonymous method is a method without a name. It allows you to define a method directly where it is used, which is especially useful when working with delegates.
            */

            // Use an anonymous method with the delegate
            GreetDelegate greetAnonymous = delegate (string name)
            {
                Console.WriteLine($"Hello, {name}!");
            };

            greetAnonymous("Alice");

            /*
            2. Lambda Expressions
            A lambda expression is a more concise way to write anonymous methods. 
            It uses the => syntax (called the "lambda operator") to separate the input parameters from the method body.
            */

            GreetDelegate greetLambda = (name) => Console.WriteLine($"Hello, {name}!");
            greetLambda("Bob");

            Func<int, int, int> add = (x, y) => x + y; // Func<int, int, int>: built-in delegate
            Console.WriteLine(add(3, 5));

            Func<int, int, int> multiply = (x, y) =>
            {
                Console.WriteLine($"Multiplying {x} and {y}"); // Multi-line Lambda example
                return x * y;
            };
            Console.WriteLine(multiply(3, 4));

            /*
            3. What is Delegates
            With the understanding of anonymous methods and lambdas, we can now fully explore delegates. A delegate is like a container for methods—it allows you to store a reference to a method and invoke it later.
            */

            /*
            4. Built-in Delegates
            C# provides three generic delegate types for common scenarios:
            */

            // a. Func<T, TResult>
            Func<int, int, int> addFunc = (x, y) => x + y;
            Console.WriteLine(addFunc(3, 7)); // Outputs: 10

            // b. Action<T>
            Action<string> print = message => Console.WriteLine(message);
            print("Hello, Action!"); // Outputs: Hello, Action!

            // c. Predicate<T>
            Predicate<int> isEven = x => x % 2 == 0;
            Console.WriteLine(isEven(4)); // Outputs: True

            /*
            5. Multicast Delegates
            Delegates can hold references to multiple methods, and they invoke them all in sequence.
            */

            Notify notify = PrintToConsole;
            notify += PrintToFile;

            notify("Hello, Multicast Delegates!");
        }

        // Helper methods for Notify
        static void PrintToConsole(string message) => Console.WriteLine($"Console: {message}");
        static void PrintToFile(string message) => System.IO.File.WriteAllText("log.txt", message);
    }
}

