namespace DelegatesConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
            1. What are Delegates?
                A delegate is like a container or a reference for a method.
                It stores a reference to a method (or more) and invoke it later.
            */

            /*

            2. Use of delegates
                A) With Anonymous Methods: DelegateType delegateName = delegate (parameters) {body}
                B) With lambda: DelegateType delegateName = (parameters) => expression or statements
            */

            /*

            3. Anonymous Methods
                A method without a name.
                Allows define a method directly where it is used, which is useful when working with delegates.
                Like Lambdas, but with delegate keyword before (parameters) + normal method body between {}s
            */

            GreetDelegate greetAnonymous = delegate (string name)
            {
                Console.WriteLine($"Hello, {name}!");
            };

            greetAnonymous("Alice");

            /*
            4. Lambda Expressions
                A lambda expression is a more concise way to write anonymous methods. 
                (Input parameters) => expression or statements
            */

            GreetDelegate greetLambda = (name) => Console.WriteLine($"Hello, {name}!");
            greetLambda("Bob");

            /*
            
            5. Built-in Delegates C# provides three generic delegate types for common scenarios:
            */

            // a. Action<T1,T2,...,Tn> : up to 16 parameters, returns void
            Action<string, string> print = (strOne, strTwo) => Console.WriteLine($"{strOne} {strTwo}");
            print("Ahmed", "Mohamed");

            // b. Func<T1,T2,...,Tn, TResult> : up to 16 parameters, returns TResult
            Func<int, int, int> addFunc = (x, y) => x + y;
            Console.WriteLine(addFunc(3, 7));

            // c. Predicate<T> : Takes one parameter, returns bool, equivalent to Func<T, bool>.
            Predicate<int> isEven = x => x % 2 == 0;
            Console.WriteLine(isEven(4));

            /*
            6. Multicast Delegates
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

