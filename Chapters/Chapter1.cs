using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CandideTextAdventure;
namespace CandideTextAdventure.Chapter1
{
    class BeginningInfoDump :InfoDumpRoom
    {
        public BeginningInfoDump() : base(new InfoDumpPainting[]
                                              {
                                                  new InfoDumpPainting("Voltaire",
                                                                       "François-Marie Arouet, also known by his pen name, Voltaire, was an 18th century French philosopher. In a time period dominated by optimisitic principles, he satirized many major doctrines, including several religions and political groups. "),
                                                  new InfoDumpPainting("Leibniz", "Gottfried Wilhelm von Leibniz was a German philosopher who pioneered the idea of philosophical optimism. Also a mathematician, he invented the first mass-produced calculator. His idea of philosophical optimism had enormous influence on the writings and ideas of later philosophers, including Voltaire. Philosophical optimism is the principle that due to the presumed fact that a perfect, all knowing God created the world, that everything in the world and produced by the world is/was perfect. "),
                                                  //new InfoDumpPainting(""), 
                                              }, new CandidesBedroom(), "On to the real adventure!",
                                          "Welcome to Candide Text Adventure created by Shea Polansky, featuring original compositions/arrangements by Paul Hafely. Text by Paul Hafely and Shea Polansky.  You find yourself in a room with five portraits in it. The first is a portrait of Voltaire. The second is a depiction of a French salon. The third concerns itself with Leibniz. The fourth shows an auto-da-fe characteristic of the Spanish Inquisition (expected by nobody). The final represents a clash between protestant and catholic adherents in the 1700’s. "
            )
        {
            
        }
    }

    class CandidesBedroom : Room
    {
        private Clothes clothes;
        public CandidesBedroom()
        {
            Exits.Add(new CandidesHallway(this));
            Names.Add("a door to your bedroom");
            Names.Add("bedroom");
            Items.Add(new GenericItem(new string[] {"a chair", "the chair", "chair"}, "a chair",
                                      "This is a chair. It's not very comfortable"));
            Items.Add(new GenericItem(new string[] {"genealogy", "the genealogy"}, "a genealogy", "A genealogy chronicling your heritage. Sadly, you only boast seventy-one generations of nobility."));
            clothes = new Clothes();
            Items.Add(clothes);
        }
        public override void OnEnter()
        {
            Console.WriteLine("You awake to the sound of your maid, Paquette, yelling for you to wake up.");
            Console.WriteLine(
                "As you awaken, Paquette, tells you to get your clothes on, and to follow her to the sitting room for your morning lesson with Dr. Pangloss.");
        }
        public override bool AttemptedExit(Room r)
        {
            if ((CurrentRoom.Items.Contains(clothes) || Inventory.Contains(clothes)))
            {
                Console.WriteLine("Wait! You can't leave naked!");
                return false;
            }
            else return true;
        }
    }

    class CandidesBed :Item
    {
        public bool haskey = false;
        public CandidesBed()
        {
            ValidNames.AddRange(new string[] {"bed", "the bed", "a bed"});
        }
        public override string GetName()
        {
            return "a bed";
        }
        public override bool OnInteract(string command)
        {
            if (command == "examine")
            {
                Console.WriteLine("This is your bed. You sleep here. It's not very comfortable.");
                if (haskey)
                {
                    Console.WriteLine("Wait! There's something under the bed! It's a key!");
                    Room.CurrentRoom.Items.Add(new Key());
                }
            }
            return false;
        }
    }

    class Key : Item
    {
        public Key()
        {
            ValidNames.AddRange(new string[] {"key", "the key"});
        }
        public override bool AttemptedGrab()
        {
            Room.CurrentRoom.Items.Remove(this);
            return true;
        }
        public override bool AttemptedDoubleItemUse(Item target)
        {
            if (target.GetType() == typeof(LockedDoor))
            {
                Room.CurrentRoom.Exits.Add(new Room());
                Room.CurrentRoom.Items.Remove(target);
                Room.Inventory.Remove(this);
                Console.WriteLine("You unlock the door.");
                return true;
            }
            return false;

        }
        public override bool OnInteract(string command)
        {
            if (command == "examine")
            {
                Console.WriteLine("The key to the exit.");
                return true;
            }
            return false;
        }
        public override string GetName()
        {
            return "a key";
        }
    }

    class Clothes : Item
    {
        public Clothes()
        {
            ValidNames.Add("clothes");
            ValidNames.Add("the clothes");
            propername = "a suit of clothes";
        }
        public override bool AttemptedGrab()
        {
            Room.CurrentRoom.Items.Remove(this);
            return true;
        }
        public override bool AttemptedSingleUse()
        {
            Console.WriteLine("You put on the clothes.");
            Room.Inventory.Remove(this);
            return true;
        }
        public override bool OnInteract(string command)
        {
            switch(command)
            {
                case "examine":
                    Console.WriteLine("A set of plain clothes.");
                    return true;
                case "wear":
                    Console.WriteLine("You put on the clothes.");
                    Room.CurrentRoom.Items.Remove(this);
                    return true;
                default:
                    return false;
            }
        }

    }

    class LockedDoor : GenericItem
    {
        public LockedDoor() :base(new string[]{"a door", "door", "the door"}, "a door", "A door. It's locked")
        {
            
        }
    }

    class CandidesHallway : Room
    {
        public CandidesHallway(CandidesBedroom bedroom)
        {
            Exits.Add(new CandidesExit());
            Exits.Add(bedroom);
        }
    }

    class CandidesExit : Room
    {
        
    }
}
