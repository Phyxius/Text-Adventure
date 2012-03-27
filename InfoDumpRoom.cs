using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CandideTextAdventure
{
    class InfoDumpRoom : Room
    {
        private Dictionary<InfoDumpPainting, bool> paintings = new Dictionary<InfoDumpPainting, bool>(); 
        private Room next;
        public InfoDumpRoom(IEnumerable<InfoDumpPainting> list, Room nextroom)
        {
            foreach (InfoDumpPainting p in list)
                paintings.Add(p, false);
            Objects.AddRange(list);
            next = nextroom;
            Objects.Add(new TestPickup());
        }
        public override bool OnInteract(string command, Item target)
        {
            if (command == "examine" && target.GetType() == typeof(InfoDumpPainting) && paintings.ContainsKey((InfoDumpPainting)target))
            {
                paintings[(InfoDumpPainting) target] = true;
                if (!paintings.ContainsValue(false))
                {
                    target.OnInteract("examine");
                    Console.WriteLine("You feel yourself being sucked through a magical portal...");
                    ChangeRoom(next);
                    return false;
                }   
            }
            return true;
        }
        public override void OnEnter()
        {
            Console.WriteLine("Examine all paintings to continue");
        }
        public override bool OnExamine()
        {
            Console.WriteLine("You are in a room with paintings.");
            return true;
        }
    }

    class InfoDumpPainting : Item
    {
        private readonly string description;
        public InfoDumpPainting(string name, string description)
        {
            this.description = description;
            ValidNames.Add(name);
            ValidNames.Add("painting of " + name);
            ValidNames.Add("a painting of " + name);
        }
        public override bool OnInteract(string command)
        {
            switch (command)
            {
                case "examine":
                    Console.WriteLine(description);
                    return true;
                default:
                    return false;
            }
        }
        public override string GetName()
        {
            return "a painting of " + ValidNames[0];
        }
    }

    class TestPickup : Item
    {
        public TestPickup(string name = "item")
        {
            ValidNames.Add(name);
        }
        public override bool AttemptedGrab()
        {
            Console.WriteLine("You pick it up.");
            Room.Inventory.Add(this);
            Room.CurrentRoom.Objects.Remove(this);
            return true;
        }
    }
}
