using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CandideTextAdventure.Chapter1;
using SFML.Audio;

namespace CandideTextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Room.ChangeRoom(new BeginningInfoDump());
           while(true)
           {
               Console.Write(">");
               Room.ParseInput(Console.ReadLine());

           }
        }
    }
}
