﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CandideTextAdventure.Chapters
{
    internal class Credits : Room
    {
        public override void OnEnter()
        {
            MusicSystem.MusicSystem.ChangeSong("Semper Fidelis.ogg");
            var credits = new List<string>();
            credits.Add("Candide: Text Adventure Edition Credits");
            credits.Add("Programming By:\n\nShea Polansky");
            credits.Add("Music Arranged and Composed By:\n\nPaul Hafley");
            credits.Add("Written By:\n\nShea Polansky and Paul Hafley");
            credits.Add("Game Designed by:\n\nShea Polansky");
            credits.Add("Next-Generation Graphics by:\n\nShea Polansky");
            credits.Add("Beta Tested by:\n\nBrooke Polansky");
            credits.Add("Special Thanks to:\n\nMrs. Dobeck");
            credits.Add("Written in C# 4.0\nUsing SFML 2.0 and Microsoft .NET Framework 4.0");
            credits.Add("Approximate Length:\n\n3,000 Lines of Code");
            string str = "Playing Time:\n\n";
            TimeSpan tmp = DateTime.Now - MainThread.StartTime;
            tmp = tmp.Duration();
            str += Math.Round(tmp.TotalHours) + ":" + tmp.Minutes + ":" + tmp.Seconds;
            credits.Add(str);
            credits.Add("Thanks for Playing!");
            
            foreach (string s in credits)
            {
                Console.Clear();
                string[] split = s.Split('\n');
                Console.CursorTop = Console.WindowHeight/2 - split.Count()/2;
                foreach (string t in split)
                {
                    Console.CursorLeft = Console.BufferWidth/2 - t.Length/2;
                    foreach (char c in t)
                    {
                        Console.Write(c);
                        Thread.Sleep(20);
                    }
                    Console.WriteLine();
                }
                Console.CursorTop = Console.WindowHeight - 1;
                Thread.Sleep(5000);
            }
            Terminal.Pause("Press any key to exit...");
            MainThread.ContinueRunning = false;
        }
    }
}