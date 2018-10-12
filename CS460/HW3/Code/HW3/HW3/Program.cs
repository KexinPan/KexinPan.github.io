using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    class Program
    {
        static LinkedList<string> generateBinaryRepresentationList(int n)
        {
            ///<value> 
            ///Create an empty queue of strings with which to perform the traversal
            ///</value>
            LinkedQueue<StringBuilder> q = new LinkedQueue<StringBuilder>();
            ///<value> 
            ///A list for returning the binary values
            ///</value>
            LinkedList<String> output = new LinkedList<String>();
            if (n < 1)
            {
                ///<return>
                /// binary representation of negative values is not supported return an empty list
                ///</return>
                return output;
            }
            /// Enqueue the first binary number.  Use a dynamic string to avoid string concat
            q.push(new StringBuilder("1"));

            /// BFS 
            while (n-- > 0)
            {
                /// print the front of queue 
                StringBuilder sb = q.pop();
                output.AddLast(sb.ToString());
                /// Make a copy
                StringBuilder sbc = new StringBuilder(sb.ToString());
                /// Left child
                sb.Append('0');
                q.push(sb);
                /// Right child
                sbc.Append('1');
                q.push(sbc);

            }
            return output;
        }


        ///<summary>
        /// Driver program to test above function 
        ///</summary>
        static void Main(string[] args)
        {
            int n;
            if (args.Length < 1)
            {
                Console.WriteLine("Please invoke with the max value to print binary up to, like this:");
                Console.WriteLine("\tMain.exe 12");
                return;
            }
            try
            {  /// convert string to int
                n = System.Convert.ToInt32(args[0]);
            }
            catch (FormatException e)
            {
                Console.WriteLine("I'm sorry, I can't understand the number: " + args[0]);
                return;
            }
            LinkedList<String> output = generateBinaryRepresentationList(n);

            ///<summary>
            ///Print it right justified.  Longest string is the last one.
            ///Print enough spaces to move it over the correct distance
            ///</summary>
            int maxLength = output.Last.Value.Length;
            foreach (String s in output)
            {
                for (int i = 0; i < maxLength - s.Length; ++i)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(s);
            }

        }
    }
}
