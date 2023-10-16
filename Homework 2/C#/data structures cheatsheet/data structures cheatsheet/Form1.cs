using System;
using System.Collections.Generic;
using System.Collections;

namespace data_structures_cheatsheet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ARRAYS

            int[] array = new int[5];

            // add elements
            array[0] = 1;
            array[1] = 2;
            array[2] = 3;
            array[3] = 4;
            array[4] = 5;

            // loop
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }

            // remove elements 
            int removeIndex = 2;
            int[] newArray = new int[4];
            for(int i = 0; i < newArray.Length; i++)
            {
                if (i == removeIndex) continue;

                newArray[i] = array[i];
            }

            // check existance
            array.Contains(0);

            // retreive elements
            Console.WriteLine(array[0]);
            Console.WriteLine(array[1]);









            /*SORTED LIST*/

            // initialization
            SortedList sl = new SortedList();

            // add elements
            sl.Add("a", 1);
            sl.Add("b", 6);
            sl.Add("c", 0);
            sl.Add("d", 0);

            // loop 
            foreach (DictionaryEntry pair in sl)
                Console.WriteLine("{0} and {1}", pair.Key, pair.Value);

            // remove elements 
            sl.Remove(6);
            sl.Remove(0);

            // check existance
            sl.ContainsKey(0);

            // retreive elements
            Console.WriteLine(sl["a"]);
            Console.WriteLine(sl["b"]);







            /*DICTIONARY*/

            // initialization
            Dictionary<string, int> dict = new Dictionary<string, int>();

            // add elements
            dict.Add("Age", 23);
            dict.Add("Height", 172);

            // loop
            foreach(KeyValuePair<string, int> kvp in dict)
                Console.WriteLine("{0} and {1}", kvp.Key, kvp.Value);

            // remove elements 
            dict.Remove("Age");
            dict.Remove("Height");

            // check existance
            dict.ContainsKey("Age");

            // retreive elements
            Console.WriteLine(dict["Age"]);
            Console.WriteLine(dict["Height"]);





            /*HASH SET*/
            // initialization
            HashSet<int> hs = new HashSet<int>();

            // add elements
            hs.Add(1);
            hs.Add(64);
            hs.Add(128);

            // loop 
            foreach(int i in hs)
                Console.WriteLine(i);

            // remove elements 
            hs.Remove(1);      
            hs.Remove(64);   

            // check existance
            hs.Contains(1);

            // retreive elements
            Console.WriteLine();






            /*STACK*/

            // initialization
            var stack = new Stack();

            // add elements
            stack.Push(1);
            stack.Push(6);
            stack.Push(0);
            stack.Push(0);

            // loop 
            foreach (Object o in stack)
                Console.WriteLine(o);

            // remove elements 
            stack.Pop();

            // check existance
            stack.Contains(0);

            //retreive element
            Console.WriteLine(stack.Peek());








            /*QUEUE*/

            // initialization
            var queue = new Queue();

            // add elements
            queue.Enqueue(1);
            queue.Enqueue(6);
            queue.Enqueue(0);
            queue.Enqueue(0);

            // loop 
            foreach (Object o in queue)
                Console.WriteLine(o);

            // remove elements 
            queue.Dequeue();
            queue.Dequeue();

            // check existance
            queue.Contains(0);

            // retreive elements
            Console.WriteLine(queue.Peek());







            /* LINKED LIST */

            // initialization
            LinkedList<int> ll = new LinkedList<int>();

            // add elements
            ll.AddLast(1);
            ll.AddFirst(6);
            ll.AddAfter(ll.Find(6), 4);

            // loop
            foreach (int str in ll)
            {
                Console.WriteLine(str);
            }

            // remove elements
            ll.RemoveLast();
            ll.RemoveFirst();

            // check existance
            ll.Find(7);

            // retrieve elements
            Console.WriteLine(ll.Find(4));
        }
    }
}