using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CandideTextAdventure
{
    internal class InfoDumpRoom : Room
    {
        private Dictionary<InfoDumpPainting, bool> paintings = new Dictionary<InfoDumpPainting, bool>();
        private Room next;
        private string ending, desc;

        public InfoDumpRoom(IEnumerable<InfoDumpPainting> list, Room nextroom, string endingmessage = "You feel yourself being sucked through a magical portal...", string OpeningDescription = "Blah.")
        {
            foreach (InfoDumpPainting p in list)
                paintings.Add(p, false);
            Items.AddRange(list);
            next = nextroom;
            ending = endingmessage;
            desc = OpeningDescription;
            //Items.Add(new TestPickup());
            //Items.Add(new TestUsable());
        }

        public override bool OnInteract(string command, Item target, string attemptedname)
        {
            if (command == "examine" && target.GetType() == typeof (InfoDumpPainting) &&
                paintings.ContainsKey((InfoDumpPainting) target))
            {
                paintings[(InfoDumpPainting) target] = true;
                if (!paintings.ContainsValue(false))
                {
                    target.OnInteract("examine", attemptedname);
                    Terminal.WriteLine(ending);
                    Terminal.WriteLine("Press any key to continue...");
                    Terminal.ReadKey();
                    Terminal.Clear();
                    ChangeRoom(next);
                    return false;
                }
            }
            return true;
        }

        public override void OnEnter()
        {
            Terminal.WriteLine("Examine all paintings to continue");
        }

        public override bool OnExamine()
        {
            Terminal.WriteLine("You are in a room with paintings.");
            return true;
        }

        public override void Describe(bool isFirstEntry = false)
        {
            Terminal.WriteLine(desc);
        }


    }

    internal class InfoDumpPainting : Item
    {
        private readonly string description;
        private string name;
        public InfoDumpPainting(string name, string description)
        {
            this.description = description;
            this.name = name;
            ValidNames.Add(name.ToLower());
            ValidNames.Add("painting of " + name.ToLower());
            ValidNames.Add("a painting of " + name.ToLower());
        }

        public override bool OnInteract(string command, string attemptedname)
        {
            switch (command)
            {
                case "examine":
                    Terminal.WriteLine(description);
                    return true;
                default:
                    return false;
            }
        }

        public override string GetName()
        {
            return "a painting of " + name;
        }
    }

    internal class TestPickup : Item
    {
        public TestPickup(string name = "item")
        {
            ValidNames.Add(name);
        }

        public override bool AttemptedGrab()
        {
            Terminal.WriteLine("You pick it up.");
            Room.Inventory.Add(this);
            Room.CurrentRoom.Items.Remove(this);
            return true;
        }
        public override bool AttemptedSingleUse()
        {
            Terminal.WriteLine("You use it on itself");
            return true;
        }

        public override bool OnInteract(string command, string attemptedname)
        {
            if (command == "examine")
            {
                Terminal.WriteLine("It is an item.");
                return true;
            }
            return false;
        }
        public override bool AttemptedDoubleItemUse(Item target)
        {
            Terminal.WriteLine("You use it.");
            return true;
        }
    }

    class TestUsable : Item
    {
        public TestUsable(string name = "usable")
        {
            ValidNames.Add(name);
        }
        
    }
}