using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CandideTextAdventure
{
    partial class Room
    {
        public List<Item> Objects = new List<Item>();
        public List<Room> Exits = new List<Room>();
        public List<string> Names = new List<string>();
        public virtual bool OnInteract(string command, Item target)
        {
            return true;
        }
        public virtual bool OnExamine()
        {
            Console.WriteLine("Just an empty dev room. How did you get here?");
            return true;
        }
        public virtual string GetName()
        {
            return "a door to a dev room";
        }
        public virtual void OnEnter()
        {
            return;
        }
        public virtual void OnExit()
        {
            return;
        }
        public virtual void Describe(bool isFirstEntry = false)
        {
            if (this.OnExamine() && this.Objects.Count > 0)
                ListItems(this);
            if (this.Exits.Count > 0)
                ListExits(this);
            this.OnEnter();
        }

    }
}
