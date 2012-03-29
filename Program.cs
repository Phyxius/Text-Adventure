using System;
using CandideTextAdventure.Chapter1;
using CandideTextAdventure.Chapter2;
using CandideTextAdventure.Chapter3to4;

namespace CandideTextAdventure
{
    class MainThread
    {
        public static bool ContinueRunning = true;
        static void Main(string[] args)
        {
            Console.Title = "Candide Text Adventure";
            //Console.WindowWidth = Console.LargestWindowWidth;
            Room.ChangeRoom(new BeginningInfoDump());
            //Room.ChangeRoom(new ChapterThreeBeginning());
            while (ContinueRunning)
            {
                Terminal.Write(">");
                Room.ParseInput(Terminal.ReadLine());
                Terminal.WriteLine();
            }
        }
    }
}
