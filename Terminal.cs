using System;
using System.Threading;

namespace CandideTextAdventure
{
    internal static class Terminal
    {
        public static bool UseAnimation = true;

        public static void WriteLine(string message = "")
        {
            /*var rows = message.Split('\n');
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
            }*/
            Write(message + "\n");
        }

        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        public static void Write(string s)
        {
            string[] rows = s.Split('\n');
            for (int i = 0; i < rows.Length; i++)
            {
                string[] words = rows[i].Split(' ');
                for (int j = 0; j < words.Length; j++)
                {
                    string word = words[j];
                    if (Console.BufferWidth - (Console.CursorLeft + 1) < word.Length)
                        Console.WriteLine();
                    _Write(word);
                    if (j != words.Length - 1)
                    {
                        if (Console.BufferWidth == Console.CursorLeft)
                            Console.WriteLine();
                        else _Write(" ");
                    }
                }
                if (i < rows.Length - 1)
                    Console.WriteLine();
            }
        }

        private static void _Write(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                if (UseAnimation)
                    Thread.Sleep(20);
            }
        }

        public static ConsoleKeyInfo ReadKey(bool HideKeyPressed = true)
        {
            ConsoleKeyInfo tmp = Console.ReadKey();
            if (!HideKeyPressed)
                return tmp;
            if (tmp.Key != ConsoleKey.Enter)
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
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
            Write(message);
            ReadKey();
            WriteLine();
            WriteLine();
        }

        public static void RestartFromLastCheckpoint<T>(params Item[] inventory) where T : Room, new()
        {
            MusicSystem.MusicSystem.ChangeSong("mario game over.ogg");
            WriteLine("Restart from last checkpoint?");
            Write("[y]es/[n]o: ");
            switch (ReadLine().ToLower())
            {
                case "yes":
                case "y":
                    var tmp = new T();
                    Room.ChangeRoom(tmp, true);
                    Room.Inventory.Clear();
                    foreach (Item i in inventory)
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