using System;

/*
Events: events are a mechanism for communication between objects, based on the observer pattern. 
They allow one object (the publisher) to notify other objects (subscribers) when something of interest happens. 
Events are built on delegates, which makes them flexible and type-safe.

An event is a wrapper around a delegate. While delegate represents reference to a method, event provides additional functionality:
1.It restricts access: Only the class that declares the event can raise (invoke) it.
2.It allows multiple methods to be subscribed and invoked when the event is triggered.

Key Terminology
Publisher: The object that declares and raises the event.
Subscriber: The object that listens to the event and defines the methods that are called when the event is raised.

How to Declare and Use Events
1.Define a Delegate Type: (Or use EventHandler or EventHandler<T>).
2.Declare an Event: Use the event keyword.
3.Raise the Event: Call the event to notify all subscribers.
4.Subscribe to the Event: Attach methods (event handlers) using the += operator
*/

namespace EventsConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var button = new Button();
            var subscriber = new EventSubscriber();

            // Subscribe to the event
            button.ButtonClicked += subscriber.HandleButtonClicked;

            // Trigger the event
            button.OnClick();   // Output: Button clicked!
                                //         Button was clicked - Event handled!

            button.ButtonClicked -= subscriber.HandleButtonClicked;

        }
    }
}