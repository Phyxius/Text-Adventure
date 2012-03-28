using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CandideTextAdventure.Chapter1;
using SFML.Audio;

namespace CandideTextAdventure
{
    class MainThread
    {
        public static bool ContinueRunning = true;
        static void Main(string[] args)
        {
            Room.ChangeRoom(new BeginningInfoDump());
            //Room.ChangeRoom(new Lesson());
            while (ContinueRunning)
            {
                Terminal.Write(">");
                Room.ParseInput(Terminal.ReadLine());
            }
        }
    }
}
