using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using SFML.Audio;

namespace CandideTextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Room.ChangeRoom(
                new InfoDumpRoom(
                    new InfoDumpPainting[]
                        {new InfoDumpPainting("a", "A painting of a."), new InfoDumpPainting("b", "A painting of b")},
                    new Room()));
           while(true)
           {
               Console.Write(">");
               Room.ParseInput(Console.ReadLine());

           }
        }
    }
}
