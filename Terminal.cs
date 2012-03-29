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
        public static ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }
        public static void Clear()
        {
            Console.Clear();
        }
        public static void Pause(string message = "Press any key to continue...")
        {
            WriteLine(message);
            ReadKey();
        }

        public static void RestartFromLastCheckpoint<T>() where T : Room,new()
        {
            WriteLine("Restart from last checkpoint?");
            Write("[y]es/[n]o: ");
            switch (ReadLine().ToLower())
            {
                case "yes":
                case "y":
                    Room.ChangeRoom(new T());
                    return;
                case "no":
                case "n":
                    MainThread.ContinueRunning = false;
                    return;
                default:
                    WriteLine("What?");
                    break;
            }
        }

        public static void EndOfDemo()
        {
            WriteLine();
            WriteLine("This ends the demo of the game.");
            WriteLine("I hope you have enjoyed it!");
            Pause();
            MainThread.ContinueRunning = false;
        }
    }
}
