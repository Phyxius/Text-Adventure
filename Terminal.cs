using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CandideTextAdventure
{
    static class Terminal
    {
        //TODO: Automatic word wrap
        public static void WriteLine(string message = "")
        {
            var rows = message.Split('\n');
            for (int i = 0; i < rows.Length; i++)
            {
                var words = rows[i].Split(' ');
                for (int j = 0; j < words.Length; j++)
                {
                    var word = words[j];
                    if (Console.BufferWidth - (Console.CursorLeft + 1) < word.Length)
                        Console.WriteLine();
                    Console.Write(word);
                    if (j != words.Length - 1)
                    {
                        if (Console.BufferWidth == Console.CursorLeft)
                            Console.WriteLine();
                        else Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }
        }
        public static string ReadLine()
        {
            return Console.ReadLine();
        }
        public static void Write(string s)
        {
            var rows = s.Split('\n');
            for (int i = 0; i < rows.Length; i++)
            {
                var words = rows[i].Split(' ');
                for (int j = 0; j < words.Length; j++ )
                {
                    var word = words[j];
                    if (Console.BufferWidth - (Console.CursorLeft + 1) < word.Length)
                        Console.WriteLine();
                    Console.Write(word);
                    if (j != words.Length - 1)
                    {
                        if (Console.BufferWidth == Console.CursorLeft)
                            Console.WriteLine();
                        else Console.Write(' ');
                    }
                }
                if(i < rows.Length - 1)
                    Console.WriteLine();
            }
        }
        public static ConsoleKeyInfo ReadKey(bool HideKeyPressed = true)
        {
            var tmp = Console.ReadKey();
            if (!HideKeyPressed)
                return tmp;
            if(Char.IsLetter(tmp.KeyChar))
                Console.CursorLeft--;
            Console.Write(' ');
            Console.CursorLeft--;
            return tmp;
        }
        public static void Clear()
        {
            Console.Clear();
        }
        public static void Pause(string message = "Press any key to continue...")
        {
            WriteLine(message);
            ReadKey();
            WriteLine();
        }

        public static void RestartFromLastCheckpoint<T>(params Item[] inventory) where T : Room,new()
        {
            WriteLine("Restart from last checkpoint?");
            Write("[y]es/[n]o: ");
            switch (ReadLine().ToLower())
            {
                case "yes":
                case "y":
                    var tmp = new T();    
                    Room.ChangeRoom(tmp, true);
                    Room.Inventory.Clear();
                    foreach(Item i in inventory)
                        Room.Inventory.Add(i);
                    return;
                case "no":
                case "n":
                    MainThread.ContinueRunning = false;
                    return;
                default:
                    WriteLine("What?");
                    RestartFromLastCheckpoint<T>(inventory);
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
