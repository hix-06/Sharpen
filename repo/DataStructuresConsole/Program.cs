using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructuresConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 1. Array: Fixed size, strongly typed
            int[] numbersArray = new int[] { 4, 5, 6, 5 };
            Console.WriteLine($"Length: {numbersArray.Length}"); // Number of elements
            Console.WriteLine($"Rank: {numbersArray.Rank}"); // Number of dimensions
            Array.Sort(numbersArray); // Sorts the array
            Array.Reverse(numbersArray); // Reverses the array
            int index = Array.IndexOf(numbersArray, 5); // Finds the index of the first occurrence of 5
            Console.WriteLine($"Index of 5: {index}");
            bool exists = Array.Exists(numbersArray, element => element == 6); // Checks if 6 exists
            Console.WriteLine($"6 exists: {exists}");
            int found = Array.Find(numbersArray, element => element > 4); // Finds the first element greater than 4
            Console.WriteLine($"First element > 4: {found}");
            int[] subArray = new int[2];
            Array.Copy(numbersArray, 1, subArray, 0, 2); // Copies 2 elements starting from index 1
            Console.WriteLine("SubArray:");
            foreach (int num in subArray) Console.Write($"{num} ");
            Console.WriteLine("\n");

            // 2. ArrayList: Dynamic size, not strongly typed
            ArrayList myArrayList = new ArrayList { "Hi", 5, DateTime.Now, "Hello" };
            Console.WriteLine($"Capacity: {myArrayList.Capacity}"); // Current capacity
            myArrayList.Add(10); // Adds an element
            myArrayList.Insert(2, "Inserted at index 2"); // Inserts at index 2
            myArrayList.Remove("Hello"); // Removes the first occurrence of "Hello"
            myArrayList.RemoveAt(0); // Removes the element at index 0
            myArrayList.Sort(); // Sorts the ArrayList (elements must be of the same type)
            myArrayList.Reverse(); // Reverses the ArrayList
            Console.WriteLine("ArrayList elements:");
            foreach (object obj in myArrayList) Console.Write($"{obj} ");
            Console.WriteLine("\n");

            // 3. List<T>: Dynamic size, strongly typed
            List<int> numbersList = new List<int> { 1, 2, 3, 4 };
            numbersList.Add(5); // Adds an element
            numbersList.AddRange(new int[] { 6, 7 }); // Adds multiple elements
            numbersList.Insert(2, 10); // Inserts 10 at index 2
            numbersList.Remove(3); // Removes the first occurrence of 3
            numbersList.RemoveAt(0); // Removes the element at index 0
            numbersList.Sort(); // Sorts the list
            numbersList.Reverse(); // Reverses the list
            bool contains = numbersList.Contains(5); // Checks if 5 is in the list
            Console.WriteLine($"List contains 5: {contains}");
            int listIndex = numbersList.IndexOf(7); // Finds the index of 7
            Console.WriteLine($"Index of 7: {listIndex}");
            int[] listArray = numbersList.ToArray(); // Converts to array
            Console.WriteLine("List elements:");
            foreach (int num in numbersList) Console.Write($"{num} ");
            Console.WriteLine("\n");

            // 4. Hashtable: Key-value pairs, not strongly typed
            Hashtable table = new Hashtable
            {
                { "key1", "value1" },
                { "key2", "value2" }
            };
            table.Add("key3", "value3"); // Adds a new key-value pair
            table.Remove("key2"); // Removes the entry with key "key2"
            bool hasKey = table.ContainsKey("key1"); // Checks if "key1" exists
            Console.WriteLine($"Hashtable contains 'key1': {hasKey}");
            bool hasValue = table.ContainsValue("value3"); // Checks if "value3" exists
            Console.WriteLine($"Hashtable contains 'value3': {hasValue}");
            Console.WriteLine("Hashtable elements:");
            foreach (DictionaryEntry entry in table) Console.WriteLine($"{entry.Key}: {entry.Value}");
            Console.WriteLine();

            // 5. Dictionary<TKey, TValue>: Strongly typed key-value pairs
            Dictionary<string, int> dictionary = new Dictionary<string, int>
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 }
            };
            dictionary.Add("four", 4); // Adds a new key-value pair
            dictionary.Remove("two"); // Removes the entry with key "two"
            bool dictHasKey = dictionary.ContainsKey("three"); // Checks if "three" exists
            Console.WriteLine($"Dictionary contains 'three': {dictHasKey}");
            bool dictHasValue = dictionary.ContainsValue(4); // Checks if value 4 exists
            Console.WriteLine($"Dictionary contains value 4: {dictHasValue}");
            int value;
            if (dictionary.TryGetValue("one", out value))
            {
                Console.WriteLine($"Value associated with 'one': {value}");
            }
            Console.WriteLine("Dictionary elements:");
            foreach (var kvp in dictionary) Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            Console.WriteLine();

            // 6. Queue: FIFO collection, not strongly typed
            Queue queue = new Queue();
            queue.Enqueue(1); // Adds an element to the end
            queue.Enqueue("two");
            queue.Enqueue(3.0);
            Console.WriteLine($"Queue count: {queue.Count}");
            object first = queue.Peek(); // Returns the first element without removing
            Console.WriteLine($"First element: {first}");
            object dequeued = queue.Dequeue(); // Removes and returns the first element
            Console.WriteLine($"Dequeued element: {dequeued}");
            Console.WriteLine("Queue elements:");
            foreach (object obj in queue) Console.Write($"{obj} ");
            Console.WriteLine("\n");

            // 7. Queue<T>: FIFO collection, strongly typed
            Queue<int> queueT = new Queue<int>();
            queueT.Enqueue(1); // Adds an element to the end
            queueT.Enqueue(2);
            queueT.Enqueue(3);
            Console.WriteLine($"Queue<T> count: {queueT.Count}");
            int firstT = queueT.Peek(); // Returns the first element without removing
            Console.WriteLine($"First element: {firstT}");
            int dequeuedT = queueT.Dequeue(); // Removes and returns the first element
            Console.WriteLine($"Dequeued element: {dequeuedT}");
            Console.WriteLine("Queue<T> elements:");
            foreach (int num in queueT) Console.Write($"{num} ");
            Console.WriteLine("\n");

            // 8. Stack: LIFO collection, not strongly typed
            Stack stack = new Stack();
            stack.Push(1); // Adds an element to the top
            stack.Push("two");
            stack.Push(3.0);
            Console.WriteLine($"Stack count: {stack.Count}");
            object top = stack.Peek(); // Returns the top element without removing
            Console.WriteLine($"Top element: {top}");
            object popped = stack.Pop(); // Removes and returns the top element
            Console.WriteLine($"Popped element: {popped}");
            Console.WriteLine("Stack elements:");
            foreach (object obj in stack) Console.Write($"{obj} ");
            Console.WriteLine("\n");

            // 9. Stack<T>: LIFO collection, strongly typed
            Stack<int> stackT = new Stack<int>();
            stackT.Push(1); // Adds an element to the top
            stackT.Push(2);
            stackT.Push(3);
            Console.WriteLine($"Stack<T> count: {stackT.Count}");
            int topT = stackT.Peek(); // Returns the top element without removing
            Console.WriteLine($"Top element: {topT}");
            int poppedT = stackT.Pop(); // Removes and returns the top element
            Console.WriteLine($"Popped element: {poppedT}");
            Console.WriteLine("Stack<T> elements:");
            foreach (int num in stackT) Console.Write($"{num} ");
            Console.WriteLine("\n");

            // 10. LinkedList<T>: Doubly linked list, strongly typed
            LinkedList<string> linkedList = new LinkedList<string>();
            linkedList.AddLast("first"); // Adds an element to the end
            linkedList.AddFirst("zeroth"); // Adds an element to the beginning
            linkedList.AddLast("second");
            linkedList.Remove("zeroth"); // Removes the first occurrence of the element
            LinkedListNode<string> node = linkedList.Find("first"); // Finds the node with the value "first"
            linkedList.AddAfter(node, "between first and second"); // Adds after a specific node
            Console.WriteLine("LinkedList elements:");
            foreach (string str in linkedList) Console.Write($"{str} ");
            Console.WriteLine("\n");

            // 11. HashSet<T>: Unique elements, unordered, strongly typed
            HashSet<int> hashSet = new HashSet<int> { 1, 2, 3 };
            hashSet.Add(3); // Duplicate elements are ignored
            hashSet.Add(4); // Adds a unique element
            hashSet.Remove(2); // Removes the element 2
            bool containsHashSet = hashSet.Contains(3); // Checks if 3 exists
            Console.WriteLine($"HashSet contains 3: {containsHashSet}");
            Console.WriteLine("HashSet elements:");
            foreach (int num in hashSet) Console.Write($"{num} ");
            Console.WriteLine("\n");

            // 12. SortedList: Non-generic key-value pair collection, sorted by keys
            SortedList sortedList = new SortedList();
            sortedList.Add("one", 1); // Adds a key-value pair
            sortedList.Add("two", 2);
            sortedList.Add("three", 3);
            Console.WriteLine($"Count: {sortedList.Count}"); // Number of elements
            object valueByKey = sortedList["one"]; // Accesses value by key
            Console.WriteLine($"Value for 'one': {valueByKey}");
            sortedList.Remove("two"); // Removes the entry with key "two"
            Console.WriteLine("SortedList elements:");
            foreach (DictionaryEntry entry in sortedList)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
            Console.WriteLine("\n");

            // 13. SortedSet<T>: Unique elements, sorted, strongly typed
            SortedSet<int> sortedSet = new SortedSet<int> { 5, 3, 1, 4, 2 };
            sortedSet.Add(6); // Adds a unique element
            sortedSet.Remove(1); // Removes the element 1
            bool containsSortedSet = sortedSet.Contains(4); // Checks if 4 exists
            Console.WriteLine($"SortedSet contains 4: {containsSortedSet}");
            Console.WriteLine("SortedSet elements:");
            foreach (int num in sortedSet) Console.Write($"{num} ");
            Console.WriteLine("\n");
        }
    }
}

/*

*/