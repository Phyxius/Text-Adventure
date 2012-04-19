using System;
using CandideTextAdventure.Chapter1;

namespace CandideTextAdventure
{
    internal class MainThread
    {
        public static bool ContinueRunning = true;
        public static DateTime StartTime = DateTime.Now;

        private static void Main(string[] args)
        {
            Console.Title = "Candide: Text Adventure Edition by Shea and Paul";
            MusicSystem.MusicSystem.ChangeSong("Allemande.ogg");
            Terminal.WriteLine(
                "Welcome to Candide: Text Adventure Edition!");
            Terminal.WriteLine(
                "In this Text Adventure, you take the role of Candide and will experience his adventures.");
            Terminal.WriteLine("However, we have to dump some extra information on you first.");
            Terminal.WriteLine(
                "Also, remember a list of common commands can be accessed by typing 'help' at the prompt.");
            //Terminal.WriteLine("This text adventure was programme");
            Terminal.Pause();
            //Console.WindowWidth = Console.LargestWindowWidth;
            Room.ChangeRoom(new BeginningInfoDump());
            //Room.ChangeRoom(new Chapter16Start());
            //Room.ChangeRoom(new Credits());
            //Console.WriteLine(MusicSystem.MusicSystem.MusicDirectory);
            while (ContinueRunning)
            {
                Terminal.Write(">");
                Room.ParseInput(Terminal.ReadLine());
                //MusicSystem.MusicSystem.ChangeSong(Terminal.ReadLine());
                Terminal.WriteLine();
            }
        }
    }
}