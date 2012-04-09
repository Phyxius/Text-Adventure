using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CandideTextAdventure.Chapter11;

namespace CandideTextAdventure.Chapter5to10
{
    class Chapter5Begin : Room
    {
        private bool hastried = false;
        public Chapter5Begin()
        {
            Exits.AddRange(new Room[] {new WestRoad(), new SouthRoad(), new NorthRoad(), new EastRoad(),new WhippingPlatform(), });
            Items.Add(new CharredCorpse());
            Items.Add(new Guard());
        }

        public override void Describe(bool isFirstEntry = false)
        {
            MusicSystem.MusicSystem.ChangeSong("Asturias.ogg");
            Terminal.WriteLine(
                "John the Anabaptist is obliged by some mercantile affairs to visit Lisbon. He has asked you and your teacher to join him.");
            Terminal.WriteLine("Within sight of the port, a terrible shipwreck occurs.");
            Terminal.WriteLine("Everyone except yourself, Pangloss, and one crewman drowns.");
            Terminal.WriteLine("As you wash up on the shore, Lisbon is struck by an earthquake.");
            Terminal.WriteLine(
                "The Portuguese Inquisition decides that the best way to prevent such a thing from happening again is with a great and public auto-da-fe.");
            Terminal.WriteLine("You are sentenced to a flogging, a Jew to a burning, and Pangloss to a hanging.");
            Terminal.Pause();
            Terminal.WriteLine();
            Terminal.WriteLine("On the day of the auto-da-fe, you are led out to a public square.");
            Terminal.WriteLine("A guard tells you to go to the whipping platform in the center of the square.");
            base.Describe(isFirstEntry);
            Terminal.WriteLine("Strangely, you do not see Pangloss's corpse.");
        }

        public override bool AttemptedExit(Room target)
        {
            if (target.GetType() == typeof(WhippingPlatform) || target.GetType() == typeof(Chapter5Begin))
                return true;
            if (!hastried)
            {
                Terminal.WriteLine("As you move towards the road, the guard roughly grabs you and shoves you back.");
                Terminal.WriteLine("He says, \"Don't even think about trying to escape!\"");
                hastried = true;
                return false;
            }
            Terminal.WriteLine("As you try to escape, the guard draws his pistol and shoots you dead.");
            Terminal.RestartFromLastCheckpoint<Chapter5Begin>();
            return false;
        }
    }

    class Guard : GenericItem
    {
        public Guard() : base("a guard", "He looks very stern.", "guard", "a guard")
        {
            
        }
    }

    class CharredCorpse : GenericItem
    {
        public CharredCorpse() : base("a charred corpse", "He's dead, Jim.", "corpse", "charred corpse", "jew")
        {
            
        }
    }

    class WhippingPlatform : GenericRoom
    {
        public WhippingPlatform() : base("a whipping platform", "It looks scary. I've been told to go here.", "whipping platform", "the whipping platform", "platform")
        {
            Exits.Add(new SecondWestRoad());
            Items.Add(new Sword());
        }

        public override void  Describe(bool isFirstEntry = false)
        {
            if (isFirstEntry)
            {
                Terminal.WriteLine("You submit to your flogging.");
                Terminal.WriteLine(
                    "After your ordeal, you are released into the street. You see an old woman beckoning to you from the west.");
            }
            base.Describe(isFirstEntry);
        }
    }

    class Sword : GenericItem
    {
        public Sword() : base("a sword lying on the ground", "This sword looks very sharp. It might come in handy later.", "sword", "sword lying on the ground")
        {
            
        }

        public override bool AttemptedDoubleItemUse(Item target)
        {
            if (target.GetType() == typeof(Jew))
            {
                Room.ParseInput("stab jew");
                return true;
            }
            if (target.GetType() == typeof(Inquisitor))
            {
                Room.ParseInput("stab inquisitor");
                return true;
            }
            return false;
        }

        public override bool AttemptedGrab()
        {
            Terminal.WriteLine("You pick up the sword and put it on your waist.");
            name = "a sword";
            Room.CurrentRoom.Items.Remove(this);
            Room.Inventory.Add(this);
            return true;
        }
    }

    class NorthRoad : GenericRoom
    {
        public NorthRoad() : base("a road leading North","You can go here." , "north", "north road")
        {
            
        }
    }

    class SouthRoad : GenericRoom
    {
        public SouthRoad() : base("a road leading South", "You can go here.", "south", "south road")
        {
            
        }
    }

    class EastRoad : GenericRoom
    {
        public EastRoad() : base("a road leading East", "You can go here.", "east", "east road")
        {
            
        }
    }

    class WestRoad : GenericRoom
    {
        public WestRoad() : base ("a road leading West", "You can go here.", "west", "west road")
        {
            
        }
    }

    class SecondWestRoad : GenericRoom
    {
        public Item antagonist = new Item();
        public SecondWestRoad() :base("a road leading West", "You can go here.", "west", "west road")
        {
            antagonist = (new SecondFoodPlate());
            Items.Add(antagonist);
            Exits.Add(new Outside());
            
        }

        public override void Describe(bool isFirstEntry = false)
        {
            Terminal.WriteLine("As you approach the old woman, she ushers you into her house.");
            Terminal.WriteLine(
                "There the old woman serves you dinner, and you sit down to eat it with her. Another woman wearing a veil joins you as well.");
            Terminal.WriteLine(
                "You attempt to introduce yourself to the veiled woman, but the old one shushes you and says, \"Wait until after you eat dinner. Then all will be made clear.\"");
            base.Describe();
        }

        public override bool AttemptedExit(Room target)
        {
            if(target.GetType() == typeof(Chapter11Start))
                return true;
            var t = antagonist.GetType();
            if (t == typeof(SecondFoodPlate))
                Terminal.WriteLine("Wait! You haven't finished your dinner!");
            else if (t == typeof(Inquisitor))
            {
                Terminal.WriteLine("As you turn your back on the Inquisitor to leave, he stabs you, shouting \"Our major weapon is big pointy swords! And surprise!\"");
                Terminal.WriteLine("You die.");
                Terminal.RestartFromLastCheckpoint<Chapter5Begin>();
            }
            else if (t == typeof(Jew))
            {
                Terminal.WriteLine("As you turn your back on the Jew to leave, he stabs you.");
                Terminal.WriteLine("You die.");
                Terminal.RestartFromLastCheckpoint<Chapter5Begin>();
            }
            else throw new Exception();
            return false;
        }
    }

    class SecondFoodPlate : GenericItem
    {
        public SecondFoodPlate() :base("a plate piled high with delicious food", "This plate looks delicious! I should eat it!", "plate", "dinnerplate", "plate of food","dinnerplate full of food", "food", "meal", "dinner",
            "a metal dinnerplate full of food")
        {
            
        }

        public override bool OnInteract(string command, string attemptedname)
        {
            if (command == "eat" || command == "finish") 
            {
                if (attemptedname == "plate" || attemptedname == "dinnerplate" || attemptedname == "plate full of food")
                {
                    Terminal.WriteLine(
                        "As you bite the plate, your front teeth shatter, creating huge gashes in your gums.");
                    Terminal.WriteLine("Infection sets in a few days later, and you die.");
                    Terminal.RestartFromLastCheckpoint<Chapter5Begin>();
                    return true;
                }
                Terminal.WriteLine("You eat the food.");
                Terminal.WriteLine("After everyone is finished the veiled woman unveils herself.");
                Terminal.WriteLine("It is your Lady Cunegonde!");
                Terminal.WriteLine("You ask her what happened to her. She tells of how she was raped, enslaved, and sold to a Jew, who shares her with the Grand Inquisitor.");
                Terminal.WriteLine("You tell Lady Cunegonde your story. As you finish, the High Inquisitor walks in.");
                Terminal.WriteLine(
                    "He sees you and goes into a fit of rage, screaming, \"I have to share her with you too? I'd rather kill you!\"");
                var tmp = new Inquisitor();
                ((SecondWestRoad) (Room.CurrentRoom)).antagonist = tmp;
                Room.CurrentRoom.Items.Add(tmp);
                Room.CurrentRoom.Items.Remove(this);
                return true;
            }
            return base.OnInteract(command, attemptedname);
        }
    }

    class Outside : GenericRoom
    {
        public Outside() : base("a door leading outside", "Sunlight streams in from open door.", "outside", "door leading outside")
        {
            
        }
    }

    class Inquisitor : GenericItem
    {
        public Inquisitor() : base("the High Inquisitor of Lisbon", "He looks very angry.", "inquisitor", "high inquisitor", "the high inquisitor", "the inquisitor")
        {
            List<string> tmp = new List<string>();
            foreach (string s in ValidNames)
            {
                tmp.Add("with " + s);
                tmp.Add("to " + s);
            }
            ValidNames.AddRange(tmp);
        }

        public override bool OnInteract(string command, string attemptedname)
        {
            switch (command)
            {
                case "speak":
                case "talk":
                    Terminal.WriteLine(
                        "The High Inquisitor is not interested in speaking. As you attempt to speak with him, he takes out his sword and stabs you, screaming, \"NOBODY EXPECTS THE SPANISH INQUISITION!\"");
                    Terminal.WriteLine("You die.");
                    Terminal.RestartFromLastCheckpoint<Chapter5Begin>();
                    return true;
                case "kill":
                case "stab":
                case "murder":
                    if (Room.Inventory.Count == 0)
                    {
                        Terminal.WriteLine("With what? Some bad words? If only you had a sword...");
                        return true;
                    }
                    Terminal.WriteLine("You stab him. He dies.");
                    Terminal.WriteLine("As you finish wiping down your sword, the Jew walks in.");
                    Terminal.WriteLine("He too is enraged about your presence, and screams that he will kill you too.");
                    Room.CurrentRoom.Items.Add(new InquisitorCorpse());
                    Room.CurrentRoom.Items.Remove(this);
                    var tmp = new Jew();
                    ((SecondWestRoad) (Room.CurrentRoom)).antagonist = tmp;
                    Room.CurrentRoom.Items.Add(tmp);
                    return true;
                default:
                    return base.OnInteract(command, attemptedname);
            }
        }
    }

    class InquisitorCorpse : GenericItem
    {
        public InquisitorCorpse() : base("the Inquisitor's corpse", "He's dead, Jim.", "corpse", "inquisitor's corpse")
        {
            
        }
    }

    class Jew : GenericItem
    {
        public Jew() : base("an enraged Jew", "He seems enraged. He is also wearing a funny hat.","jew", "enraged jew", "the jew", "the enraged jew")
        {
            List<string> tmp = new List<string>();
            foreach (string s in ValidNames)
            {
                tmp.Add("with " + s);
                tmp.Add("to " + s);
            }
            ValidNames.AddRange(tmp);
        }

        public override bool OnInteract(string command, string attemptedname)
        {
            switch (command)
            {
                case "speak":
                case "talk":
                    Terminal.WriteLine(
                        "The Jew is not interested in speaking. As you attempt to speak with him, he takes out his sword and stabs you.");
                    Terminal.WriteLine("You die.");
                    Terminal.RestartFromLastCheckpoint<Chapter5Begin>();
                    return true;
                case "kill":
                case "stab":
                case "murder":
                    if (Room.Inventory.Count == 0)
                    {
                        Terminal.WriteLine("With what? Some bad words? If only you had a sword...");
                        return true;
                    }
                    MusicSystem.MusicSystem.ChangeSong("Concert_piece_No.ogg");
                    Terminal.WriteLine("You stab him. He dies.");
                    Terminal.WriteLine(
                        "The old lady suggests that the group run away before someone realizes that you killed two men. Everyone agrees, and you run away to the port.");
                    Terminal.WriteLine("Once at the port, Cunegonde notices that her jewels and money have been stolen, and you thus have no way of buying a fare.");
                    Terminal.WriteLine("Instead, you enlist in the Portuguese army as a captain, and set sail for the New World, with Cunegonde and the old woman in tow.");
                    Terminal.Pause();
                    Room.ChangeRoom(new Chapter11Start());
                    return true;
                case "burn":
                case "bake":
                    Terminal.WriteLine("Who do you think you are? Hitler?");
                    return true;
                default:
                    return base.OnInteract(command, attemptedname);
            }
        }
    }
}
