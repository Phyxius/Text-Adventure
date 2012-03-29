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
                                                  new InfoDumpPainting("salon", "French salons were the center of culture and discussion during Voltaire's time, the Enlightenment. Voltaire would have spent much time in these."),
                                                  new InfoDumpPainting("auto-da-fe", "Auto-da-fe's were performed by the Spanish and Portugese Inquisitions as acts of public penence. They were often held after a major disaster or problem, in the belief that God caused the problem to happen, and needed to be placated with prayer and burnings."),
                                                  new InfoDumpPainting("riot", "PLACEHOLDER"), 
                                              }, new CandidesBedroom(), "On to the real adventure!",
                                          "Welcome to Candide Text Adventure created by Shea Polansky, featuring original compositions/arrangements by Paul Hafely. Text by Paul Hafely and Shea Polansky.  You find yourself in a room with five portraits in it. The first is a portrait of Voltaire. The second is a depiction of a French salon. The third concerns itself with Leibniz. The fourth shows an auto-da-fe characteristic of the Spanish Inquisition (expected by nobody). The final shows a riot of some kind. "
            )
        {
            
        }
    }

    class CandidesBedroom : Room
    {
        private Clothes clothes;
        private CandidesBed bed;
        public CandidesBedroom()
        {
            Exits.Add(new CandidesHallway(this));
            Names.Add("a door to your bedroom");
            Names.Add("bedroom");
            bed = new CandidesBed();
            Items.Add(bed);
            Items.Add(new GenericItem(new string[] {"a chair", "the chair", "chair"}, "a chair",
                                      "This is a chair. It's not very comfortable"));
            Items.Add(new GenericItem(new string[] {"genealogy", "the genealogy"}, "a genealogy", "A genealogy chronicling your heritage. Sadly, you can only boast seventy-one generations of nobility."));
            clothes = new Clothes();
            Items.Add(clothes);
            propername = "the door to your bedroom";
        }
        public override void OnEnter()
        {
            if (bed.haskey)
                return;
            Terminal.WriteLine("You awaken to the sound of your maid, Paquette, yelling for you to wake up.");
            Terminal.WriteLine(
                "As you awaken, Paquette tells you to get your clothes on, and to follow her to the sitting room of the castle for your morning lesson with Dr. Pangloss.");
        }
        public override bool AttemptedExit(Room r)
        {
            if ((CurrentRoom.Items.Contains(clothes) || Inventory.Contains(clothes)))
            {
                Terminal.WriteLine("Wait! You can't leave naked!");
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
        public override bool OnInteract(string command, string attemptedname)
        {
            if (command == "examine")
            {
                Terminal.WriteLine("This is your bed. You sleep here. It's not very comfortable.");
                if (haskey)
                {
                    Terminal.WriteLine("Wait! There's something under the bed! It's a key!");
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
            Room.Inventory.Add(this);
            Terminal.WriteLine("You grab the key.");
            return true;
        }
        public override bool AttemptedDoubleItemUse(Item target)
        {
            if (target.GetType() == typeof(LockedDoor))
            {
                Room.CurrentRoom.Exits.Add(new Room());
                Room.CurrentRoom.Items.Remove(target);
                Room.Inventory.Remove(this);
                Terminal.WriteLine("You unlock the door.");
                Terminal.WriteLine("You follow Paquette to your lesson.");
                Terminal.Pause();
                Room.ChangeRoom(new Lesson());
                return true;
            }
            return false;

        }
        public override bool OnInteract(string command, string attemptedname)
        {
            if (command == "examine")
            {
                Terminal.WriteLine("The key to the exit.");
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
            ValidNames.Add("suit");
            ValidNames.Add("suit of clothes");
            propername = "a suit of clothes";
        }
        public override bool AttemptedGrab()
        {
            Room.CurrentRoom.Items.Remove(this);
            Terminal.WriteLine("You pick them up. You should probably use them now.");
            Room.Inventory.Add(this);
            return true;
        }
        public override bool AttemptedSingleUse()
        {
            Terminal.WriteLine("You put on the clothes.");
            Terminal.WriteLine("You can follow Paquette now.");
            Room.Inventory.Remove(this);
            return true;
        }
        public override bool OnInteract(string command, string attemptedname)
        {
            switch(command)
            {
                case "examine":
                    Terminal.WriteLine("A set of plain clothes.");
                    return true;
                case "wear":
                    Terminal.WriteLine("You put on the clothes.");
                    Terminal.WriteLine("You can follow Paquette now.");
                    Room.CurrentRoom.Items.Remove(this);
                    return true;
                default:
                    return false;
            }
        }

    }

    class LockedDoor : GenericItem
    {
        public LockedDoor() :base(new string[]{"a door", "door", "the door"}, "a locked door", "A door. It's locked")
        {
            
        }
    }

    class Paquette1 : GenericItem
    {
        public Paquette1():base(new string[]{"paquette"}, "paquette", "Your loyal maid.")
        {
            
        }
    }

    internal class CandidesHallway : Room
    {
        public CandidesHallway(CandidesBedroom bedroom)
        {
            Exits.Add(new CandidesExit(this));
            Exits.Add(bedroom);
            propername = "a door to a hallway";
            Names.Add("hallway");
            Names.Add("door to hallway");
            Names.Add("door to a hallway");
        }
    }

    class CandidesExit : Room
    {
        public CandidesExit(CandidesHallway h)
        {
            Exits.Add(h);
            propername = "the door to the sitting room of your house";
            Names.Add("sitting room");
            Names.Add(propername);
            Names.Add("the door to the sitting room");
            Items.Add(new LockedDoor());
            Items.Add(new Paquette1());
        }
        public override void OnEnter()
        {
            if (Inventory.Count > 0)
                Terminal.WriteLine("As you enter your sitting room, Paquette says, \"Candide, I seem to have forgotten the key. Could you go find it for me? It probably found its way under your bed again.\"");
            else
            {
                Terminal.WriteLine("Paquette says, \"that's it! Just use it on the door and we can be off!\"");
            }
            ((CandidesBed) Exits[0].Exits[1].Items[0]).haskey = true;
        }
    }

    class Lesson : Room
    {
        public Lesson()
        {
            propername = "classroom";
            Items.Add(new GenericItem(new string[] {"the textbook","textbook"}, "a textbook",
                                      "This textbook is labeled, \"Introduction to metaphysico-theologo-cosmolo-nigology\"."));
            Exits.Add(new MakeOutRoom());
        }

        public override void OnEnter()
        {

        }
        public override void Describe(bool isFirstEntry = false)
        {
            Terminal.WriteLine("After your lesson, you hear Lady Cunegonde calling you from a side room.");
            base.Describe(isFirstEntry);
        }
    }

    class MakeOutRoom :Room
    {
        public MakeOutRoom()
        {
            propername = "a door to a side room";
        }
        public override void OnEnter()
        {
            Terminal.WriteLine("As you enter, Lady Cunegonde says \"I saw Dr. Pangloss performing an 'experiment' with paquette the other day. Would you like to try it with me?");
            Terminal.WriteLine(
                "Cunegonde begins to lean toward you, and just as her lips touch yours, the Baron walks in.");
            Terminal.WriteLine("He is not amused by your 'experiment', and banishes you from his castle.");
            //Terminal.WriteLine("And this concludes the demo of this game.");
            Terminal.Pause();
            MainThread.ContinueRunning = false;
        }
    }
}
