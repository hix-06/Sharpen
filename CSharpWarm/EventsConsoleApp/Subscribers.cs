
public class EventSubscriber
{
    public void HandleButtonClicked(object c, EventArgs e)
    {
        System.Console.WriteLine("Handled using EventHandler!");
    }
}