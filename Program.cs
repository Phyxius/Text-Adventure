using System;
using CandideTextAdventure.Chapter2;

namespace CandideTextAdventure
{
    class MainThread
    {
        public static bool ContinueRunning = true;
        static void Main(string[] args)
        {
            Console.Title = "Candide Text Adventure";
            //Console.WindowWidth = Console.LargestWindowWidth;
            //Room.ChangeRoom(new BeginningInfoDump());
            Room.ChangeRoom(new ChapterTwoBegin());
            while (ContinueRunning)
            {
                Terminal.Write(">");
                Room.ParseInput(Terminal.ReadLine());
                Terminal.WriteLine();
            }
        }
    }
}
