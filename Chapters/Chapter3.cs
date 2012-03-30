using System.Collections.Generic;
using CandideTextAdventure.Chapter5to10;

namespace CandideTextAdventure.Chapter3to4
{
    class ChapterThreeBeginning : Room
    {
        public ChapterThreeBeginning()
        {
            propername = "a road leading west";
            Names.AddRange(new string[] {"west", "road", "road leading west"});
            Exits.Add(new EastRoad());
        }

        public override void Describe(bool isFirstEntry = false)
        {
            Terminal.WriteLine(
                "After escaping from the Bulgarian army, you wander the land again, and eventually find yourself in Holland.");
            Terminal.WriteLine(
                "You wander into a villiage, hoping to find something to soothe your aching hunger (because that worked out so well last time).");
            Terminal.WriteLine(
                "To the east, you hear a man finishing a sermon about giving alms to the poor. You think that this sounds very promising.");
            base.Describe(isFirstEntry);
        }
    }

    class Beggar : GenericItem
    {
        public Beggar()
            : base("a grimy beggar", "This beggar seems awfully familiar. It is hard to tell through the layers of grime and sickness.",
                "beggar", "to beggar", "the beggar", "to the beggar", "the grimy beggar", "grimy beggar", "to the grimy beggar", "pangloss", "dr. pangloss", "doctor pangloss", "to pangloss", "to doctor pangloss", "to dr. pangloss")
        {

        }

        public override bool OnInteract(string command, string attemptedname)
        {
            switch(command)
            {
                case "speak":
                case "talk":
                    if (attemptedname.Contains("pangloss"))                        
                        Terminal.WriteLine("No fair! You're not supposed to know that!\nStop trying to derail the story!");
                    else
                        Terminal.WriteLine("He doesn't seem interested in you.");
                    return true;
                default:
                    return base.OnInteract(command, attemptedname);
            }
        }
    }

    class EastRoad : Room
    {
        public EastRoad()
        {
            propername = "a road leading east";
            Names.AddRange(new string[] {"east", "road leading east", "road"});
            Exits.Add(new NorthRoad());
        }

        public override void Describe(bool isFirstEntry = false)
        {
            Terminal.WriteLine(
                "As you approach the sermon-giver, he exclaims, \"I have no patience for beggars such as yourself! Get out of my sight!\"");
            Terminal.WriteLine("After his outburst, the man storms off.");
            Terminal.WriteLine(
                "As the man disappears into his house, you see another man, who is very friendly-looking, beckon to you from the north.");
            base.Describe(isFirstEntry);
        }
    }

    class NorthRoad : Room
    {
        private bool spoken = false;
        public NorthRoad()
        {
            Items.Add(new John1());
            propername = "a road leading north";
            Names.Add("north");
            Names.Add("road leading north");
            Exits.Add(new SouthRoad(this));
        }
        public override void Describe(bool isFirstEntry = false)
        {
            if (!spoken)
            {
                Terminal.WriteLine(
                    "As you approach the man, he says, \"I saw your predicament, and I feel that you were treated wrongly. Here is two florins.\"");
                Terminal.WriteLine("He introduces himself as John the Anabaptist.");
                Terminal.WriteLine(
                    "John also offers to teach you to work in fabric-making. You fall on your knees at his feet and thank him for his generosity.");
                Terminal.WriteLine("You also remember the beggar you saw earlier.");
                Terminal.WriteLine(
                    "You think to yourself, \"That beggar has much greater need of this money than I. I should share my luck with him.\"");
                Inventory.Add(new TwoFlorins());
            }
            spoken = true;
            base.Describe(isFirstEntry);
        }
    }

    class TwoFlorins : GenericItem
    {
        public TwoFlorins()
            : base("two florins", "This is your only money in the world.", "florins", "money", "two florins")
        {
            
        }
    }

    class John1 : GenericItem
    {
        public John1():base("John the Anabaptist", "He looks very friendly.", "john", "john the anabaptist", "the anabaptist", "anabaptist") //fuck yeah params
        {
            
        }
    }

    class SouthRoad : Room
    {
        public SouthRoad(NorthRoad r)
        {
            propername = "a road leading south";
            Names.Add("south");
            Names.Add("road leading south");
            Exits.Add(r);
            Exits.Add(new WestRoad());
        }
    }

    class WestRoad :Room
    {
        public WestRoad()
        {
            Items.Add(new Pangloss1());
            propername = "a road leading west";
            Names.Add("west");
            Names.Add("road leading west");
        }
    }
    
    class Pangloss1 : GenericItem
    {
        public Pangloss1()
             : base("a grimy beggar", "This beggar seems awfully familiar. It is hard to tell through the layers of grime and sickness. I should give him some money.",
                "beggar", "to beggar", "the beggar", "to the beggar", "the grimy beggar", "grimy beggar", "to the grimy beggar", "pangloss", "dr. pangloss", "doctor pangloss", "to pangloss",
            "to doctor pangloss", "to dr. pangloss", "money to dr. pangloss")
        {
            List<string> tmp = new List<string>();
            foreach (string s in (ValidNames))
            {
                string t1 = s;
                string t2 = s;
                if (!(s.Split(' ')[0] == "to"))
                {
                    t1 = "to " + s;
                    t2 = "to " + s;
                }
                t1 = "money " + t1;
                t2 = "florins " + t2;
                tmp.Add(t1);
                tmp.Add(t2);
            }
            ValidNames.AddRange(tmp);
        }

        public override bool OnInteract(string command, string attemptedname)
        {
            switch (command)
            {
                case "speak":
                case "talk":
                    if (attemptedname.Contains("pangloss"))
                        Terminal.WriteLine("No fair! You're not supposed to know that!\nStop trying to derail the story!");
                    else
                        Terminal.WriteLine("He cries out, \"Alms for the poor, young master?\"");
                    return true;
                case "give":
                    Terminal.WriteLine("As you hand him you only money, he looks into your eyes.");
                    Terminal.WriteLine("\"Master Candide?!\" he exclaims, \"Don't you regognize me, Dr. Pangloss?!\"");
                    Terminal.WriteLine("You take Dr Pangloss to John and beg him to help Pangloss as he had helped you.");
                    Terminal.WriteLine("John does as you ask, and two months pass.");
                    Terminal.Pause();
                    Terminal.WriteLine();
                    Room.ChangeRoom(new Chapter5Begin());
                    return true;
                default:
                    return base.OnInteract(command, attemptedname);
            }
        }
    }
}
