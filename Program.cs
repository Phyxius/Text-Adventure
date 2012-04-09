using System;
using CandideTextAdventure.Chapter1;
using CandideTextAdventure.Chapter11;
using CandideTextAdventure.Chapter16;
using CandideTextAdventure.Chapter19;
using CandideTextAdventure.Chapter22;
using CandideTextAdventure.Chapter5to10;



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
            //Room.ChangeRoom(new Chapter19Start());
            while (ContinueRunning)
            {
                Terminal.Write(">");
                Room.ParseInput(Terminal.ReadLine());
                Terminal.WriteLine();
            }
        }
    }
}
