using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CandideTextAdventure
{
    static class Terminal
    {
        public static void WriteLine(string message = "")
        {
            Console.WriteLine(message);
        }
        public static string ReadLine()
        {
            return Console.ReadLine();
        }
        public static void Write(string s)
        {
            Console.Write(s);
        }
    }
}
