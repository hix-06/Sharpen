
public class Button
{
    // Define an event using event keyword and the EventHandler built-in delegate for standard event signature
    public event EventHandler ButtonClicked;

    // Method to raise the event
    public void OnClick()
    {
        System.Console.WriteLine("Button Clicked");
        ButtonClicked?.Invoke(this, EventArgs.Empty);  // Invoke the event if there are subscribers. (Pass sender and event data)
    }
}