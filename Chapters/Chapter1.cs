﻿using CandideTextAdventure.Chapter2;

namespace CandideTextAdventure.Chapter1
{
    internal class BeginningInfoDump : InfoDumpRoom
    {
        public BeginningInfoDump() : base(new[]
                                              {
                                                  new InfoDumpPainting("Voltaire",
                                                                       "François-Marie Arouet, also known by his pen name, Voltaire, was an 18th century French philosopher. In a time period dominated by optimisitic principles, he satirized many major doctrines, including several religions and political groups. ",
                                                                       "voltaire"),
                                                  new InfoDumpPainting("Leibniz",
                                                                       "Gottfried Wilhelm von Leibniz was a German philosopher who pioneered the idea of philosophical optimism. Also a mathematician, he invented the first mass-produced calculator. His idea of philosophical optimism had enormous influence on the writings and ideas of later philosophers, including Voltaire. Philosophical optimism is the principle that due to the presumed fact that a perfect, all knowing God created the world, that everything in the world and produced by the world is/was perfect. ",
                                                                       "leibniz"),
                                                  new InfoDumpPainting("a salon",
                                                                       "French salons were the center of culture and discussion during Voltaire's time, the Enlightenment. Voltaire would have spent much time in these.",
                                                                       "salon", "french salon", "a french salon",
                                                                       "a salon"),
                                                  new InfoDumpPainting("a auto-da-fe",
                                                                       "Auto-da-fe's were performed by the Spanish and Portugese Inquisitions as acts of public penence. They were often held after a major disaster or problem, in the belief that God caused the problem to happen, and needed to be placated with prayer and burnings.",
                                                                       "auto-da-fe", "an auto-da-fe"),
                                                  new InfoDumpPainting("a riot",
                                                                       "This painting depicts a clash between Catholics and Protestants, over different interpretations of the Bible. Protestantism was founded by Martin Luther, who was unhappy with sever Catholic practices including selling of indulgenecs. The Catholic Church took exception to his criticisms and attempts to start his own church, and tensions rapidly rose, although they have since died down in modern times.",
                                                                       "riot", "a riot"),
                                                  new InfoDumpPainting("a destroyed town",
                                                                       "The town of Lisbon, Portugal was rocked by an earthquake in 1755 which destroyed much of the town. A short time later, a volcano erupted, again destroying much of the town. This destruction troubled Voltaire, who went on to write Candide three years later.",
                                                                       "destroyed town", "town", "a destroyed town"),
                                              }, new CandidesBedroom(), "On to the real adventure!",
                                          "You find yourself in a room with six portraits in it. The first is a portrait of Voltaire. The second is a depiction of a French salon. The third concerns itself with Leibniz. The fourth shows an auto-da-fe characteristic of the Spanish Inquisition (expected by nobody). The fith depicts a destroyed town. The final shows a riot of some kind. \n"
            )
        {
        }

        public override void OnEnter()
        {
            Terminal.WriteLine();
        }

        public override void Describe(bool isFirstEntry = false)
        {
            base.Describe(isFirstEntry);
            Terminal.WriteLine("Examine all portraits to continue.");
        }
    }

    internal class CandidesBedroom : Room
    {
        private readonly CandidesBed bed;
        public Clothes clothes;
        private bool hasPlayed;

        public CandidesBedroom()
        {
            Exits.Add(new CandidesHallway(this));
            Names.Add("a door to your bedroom");
            Names.Add("bedroom");
            bed = new CandidesBed();
            Items.Add(bed);
            Items.Add(new GenericItem(new[] {"a chair", "the chair", "chair"}, "a chair",
                                      "This is a chair. It's not very comfortable"));
            Items.Add(new GenericItem(new[] {"genealogy", "the genealogy"}, "a genealogy",
                                      "A genealogy chronicling your heritage. Sadly, you can only boast seventy-one generations of nobility."));
            clothes = new Clothes();
            Items.Add(clothes);
            propername = "the door to your bedroom";
        }

        public override void OnEnter()
        {
            if (!hasPlayed)
                MusicSystem.MusicSystem.ChangeSong("The_Tale_of_Victor_Navorksi.ogg");
            hasPlayed = true;
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

        public override bool OnInteract(string command, Item target, string attemptedname)
        {
            if (command == "iddqd")
            {
                Items.Remove(clothes);
                ChangeRoom(new ChapterTwoBegin());
                return false;
            }
            else
                return base.OnInteract(command, target, attemptedname);
        }
    }

    internal class CandidesBed : Item
    {
        public bool haskey;

        public CandidesBed()
        {
            ValidNames.AddRange(new[] {"bed", "the bed", "a bed"});
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

    internal class Key : Item
    {
        public Key()
        {
            ValidNames.AddRange(new[] {"key", "the key"});
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
            if (target.GetType() == typeof (LockedDoor))
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

    internal class Clothes : Item
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
            switch (command)
            {
                case "examine":
                    Terminal.WriteLine("A set of plain clothes.");
                    return true;
                case "wear":
                    Terminal.WriteLine("You put on the clothes.");
                    //Terminal.WriteLine("You can follow Paquette now.");
                    Room.CurrentRoom.Items.Remove(this);
                    return true;
                default:
                    return false;
            }
        }
    }

    internal class LockedDoor : GenericItem
    {
        public LockedDoor() : base(new[] {"a door", "door", "the door"}, "a locked door", "A door. It's locked")
        {
        }
    }

    internal class Paquette1 : GenericItem
    {
        public Paquette1() : base(new[] {"paquette"}, "Paquette", "Your loyal maid.")
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

    internal class CandidesExit : Room
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
            if (Inventory.Count == 0)
                Terminal.WriteLine(
                    "As you enter your sitting room, Paquette says, \"Candide, I seem to have forgotten the key. Could you go find it for me? It probably found its way under your bed again.\"");
            else
            {
                Terminal.WriteLine("Paquette says, \"That's it! Just use it on the door and we can be off!\"");
            }
            ((CandidesBed) Exits[0].Exits[1].Items[0]).haskey = true;
        }
    }

    internal class Lesson : Room
    {
        public Lesson()
        {
            propername = "classroom";
            Items.Add(new GenericItem(new[] {"the textbook", "textbook"}, "a textbook",
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

    internal class MakeOutRoom : Room
    {
        public MakeOutRoom()
        {
            propername = "a door to a side room";
            Names.AddRange(new[] {"door", "side room", "paquette"});
        }

        public override void OnEnter()
        {
            Terminal.WriteLine(
                "As you enter, Lady Cunegonde says \"I saw Dr. Pangloss performing an 'experiment' with paquette the other day. Would you like to try it with me?");
            Terminal.WriteLine(
                "Cunegonde begins to lean toward you, and just as her lips touch yours, the Baron walks in.");
            Terminal.WriteLine("He is not amused by your 'experiment', and banishes you from his castle.");
            Terminal.Pause();
            ChangeRoom(new ChapterTwoBegin());
        }
    }
}