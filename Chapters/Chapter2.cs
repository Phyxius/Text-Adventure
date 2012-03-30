using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CandideTextAdventure.Chapter2
{
    class ChapterTwoBegin : Room
    {
        public ChapterTwoBegin()
        {
            propername = "a door leading outside";
            Names.Add("outside");
            Names.Add("door leading outside");
            Names.Add("out");
            Items.Add(new RoadSign());
            Exits.Add(new Inn(this));
        }
        public override void  Describe(bool isFirstEntry = false)
        {
            Terminal.WriteLine(
                "After your banishment from your earthly paradise of Castle Thunder-ten-tronckh, you wander the land without noticing where you are going.");
            Terminal.WriteLine("Eventually, you drag yourself to the nearest town, where you hope to get something to soothe your aching hunger.");
            Terminal.WriteLine("As you walk by the town's inn, you two men wearing blue uniforms call out to you from inside. They invite you to dine with them.");
            base.Describe(isFirstEntry);
        }
        public override bool ExamineCommand()
        {
            Terminal.WriteLine("You see light streaming in from the door.");
            return true;
        }
    }

    class RoadSign : GenericItem
    {
        public RoadSign() : base(new string[]{"sign", "roadsign", "road sign"}, "a road sign", "This sign says, \"Welcome to Wald-berghoff-trarbkdikdorff.\"")
        {
            
        }
    }

    class Inn : Room
    {
        public bool isdead = false;
        public bool hasmug;
        public Inn(ChapterTwoBegin prev)
        {
            propername = "a door to a dingy inn";
            Names.Add("dingy inn");
            Names.Add("in");
            Names.Add("inn");
            Items.Add(new Plate());
            Items.Add(new MenInBlue());
            Exits.Add(prev);
        }

        public override void Describe(bool isFirstEntry = false)
        {
            Terminal.WriteLine(
                "As you enter, the men sit you down at a table, where they set before you a delicious meal.");
            Terminal.WriteLine("The tallest one says, \"Go on, eat it! Men were made to help each other!\"");
            base.Describe(isFirstEntry);
        }

        public override bool AttemptedExit(Room target)
        {
            if (isdead)
                return true;
            Terminal.WriteLine("As you get up to leave, the blue-clad men rather roughly force you back down.");
            Terminal.WriteLine("The largest says, \"Wait just a minute boy, you've not finished your " +
                               (hasmug ? "drink" : "dinner") + "!");
            return false;
        }

        
    }

    class MenInBlue : GenericItem
    {
        public MenInBlue()
            : base(
                new string[] {"men", "men in blue", "blue-clad men", "men in blue uniforms", "blue-uniformed men"},
                "some men in blue uniforms", "These men look very stern looking. They are wearing spiffy blue uniforms."
                )
        {
            
        }
    }

    class Mug : GenericItem
    {
        public Mug() :base(new string[]{"mug", "mug of beer", "beer", "mug full of frothy beer", "drink"}, "mug full of frothy beer", "This beer looks delicious!")
        {
            
        }

        public override bool OnInteract(string command, string attemptedname)
        {
            if (command == "drink" || command == "finish")
            {
                Terminal.WriteLine("You drink to the health of the King of the Bulgars.");
                Terminal.WriteLine(
                    "The blue-clad men exclaim, \"You are now the support, the upholder, the defender and the hero of the Bulgars. Your fortune is made and your glory is assured.\"");
                Terminal.WriteLine(
                    "The men immediately proceed to clap you in irons and whisk you away to be trained in the army.");
                Terminal.Pause();
                Terminal.WriteLine();
                ((Inn)(Room.CurrentRoom)).isdead = true;
                Room.ChangeRoom(new CandideTextAdventure.Chapter3to4.ChapterThreeBeginning());
                return true;
            }
            return base.OnInteract(command, attemptedname);
        }
    }

    class Plate : GenericItem
    {
        public Plate() : base(new string[]{"plate", "dinnerplate", "plate of food","dinnerplate full of food", "food", "meal", "dinner"},
            "a metal dinnerplate full of food", "This plate full of food looks delicious! I should eat it!")
        {
            
        }

        public override bool OnInteract(string command, string attemptedname)
        {
            if (command == "eat")
            {
                if (attemptedname == "plate" || attemptedname == "dinnerplate" || attemptedname == "plate full of food")
                {
                    Terminal.WriteLine(
                        "As you bite the plate, your front teeth shatter, creating huge gashes in your gums.");
                    Terminal.WriteLine("Infection sets in a few days later, and you die.");
                    ((Inn) (Room.CurrentRoom)).isdead = true;
                    Terminal.RestartFromLastCheckpoint<ChapterTwoBegin>();
                    return true;
                }
                Terminal.WriteLine(
                    "After you finish your dinner, the blue-uniformed men take your plate away, and replace it with a mug full of beer.");
                Terminal.WriteLine(
                    "They insist that you drink to the health of the King of the Bulgars, whom you have never met, and not your Lady Cunegonde, whom you still love.");
                Room.CurrentRoom.Items.Add(new Mug());
                Room.CurrentRoom.Items.Remove(this);
                ((Inn) (Room.CurrentRoom)).hasmug = true;
                return true;
            }
            return base.OnInteract(command, attemptedname);
        }
    }
}
