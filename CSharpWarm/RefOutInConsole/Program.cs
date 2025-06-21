
public struct Point3D
{
    public int X, Y, Z;
    public Point3D(int x, int y, int z) => (X, Y, Z) = (x, y, z);
}

namespace RefOutInConsole
{
    public class Program
    {
        public void Main(string[] args)
        {
            /*
            1. ref Use Cases (Read and Write)
            Use Case: Adjusting a Value Dynamically
            Scenario: You have a discount system where you need to calculate the final price of a product.
            The price may be adjusted multiple times based on conditions.
            Why ref?
            You pass the price by reference, so the method modifies the same variable rather than returning a new value.
            Example:
            */
            decimal productPrice = 100m;  // Original price
            ApplyDiscount(ref productPrice, 0.1m);  // Apply 10% discount
            Console.WriteLine(productPrice);  // Output: 90

            /*
            2. out Use Cases (Write Only)
            Use Case: Returning Multiple Outputs
            Scenario: A function needs to return multiple results, like a division operation that returns both quotient and remainder.
            Why out?
            The variables quotient and remainder don’t need to be initialized beforehand. The method ensures they are assigned.
            Example:
            */
            int q, r;
            Divide(10, 3, out q, out r);
            Console.WriteLine($"Quotient: {q}, Remainder: {r}");  // Output: Quotient: 3, Remainder: 1

            /*
            3. in Use Cases (Read Only)
            Use Case: Ensuring Read-Only Parameters for Efficiency
            Scenario: A method processes a large structure or object (like 3D points or matrices) where you want to prevent modification but 
            avoid the cost of copying the object.
            Why in?
            It prevents accidental modifications and ensures efficiency by avoiding unnecessary copying of large objects.
            Example:
            */
            Point3D point = new Point3D(1, 2, 3);
            PrintPoint(in point);

        }


        public void ApplyDiscount(ref decimal price, decimal discountRate)
        {
            price = price - (price * discountRate);
        }
        public void Divide(int dividend, int divisor, out int quotient, out int remainder)
        {
            quotient = dividend / divisor;
            remainder = dividend % divisor;
        }
        public void PrintPoint(in Point3D point)
        {
            Console.WriteLine($"Point: {point.X}, {point.Y}, {point.Z}");
            // point.X = 10;  // Error: Cannot modify read-only parameter
        }

    }
}