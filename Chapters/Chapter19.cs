using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CandideTextAdventure.Chapter22;

namespace CandideTextAdventure.Chapter19
{
    internal class Chapter19Start : GenericRoom
    {
        public Chapter19Start()
            : base(
                "a road leading to the Town Square", "You can go here.", "town square",
                "road leading to the town square", "a road leading to the town square")
        {
            Items.Add(new Roadsign());
            var tmp = new Harbor(this);
            Exits.Add(new InnRoad(this, tmp));
            Exits.Add(tmp);
        }
    }

    internal class Roadsign : GenericItem
    {
        public Roadsign()
            : base(
                "a shabby-looking sign", "This sign says, \"Welcome to Surinam!\"", "sign", "roadsign",
                "shabby road sign", "road sign", "shabby road sign")
        {

        }
    }

    internal class InnRoad : GenericRoom
    {
        public InnRoad(Chapter19Start prev, Harbor harbor)
            : base("an inn", "You can go here.", "inn", "an inn")
        {
            Exits.Add(prev);
        }

        public override void Describe(bool isFirstEntry = false)
        {
            Terminal.WriteLine("You don't need to go here right now.");
            ChangeRoom(Exits[0]);
        }
    }

    internal class Harbor : GenericRoom
    {
        public Harbor(Chapter19Start prev)
            : base(
                "the town's harbor", "You can go here.", "harbor", "the town's harbor", "town's harbor",
                "the towns harbor", "towns harbor")
        {
            Exits.Add(prev);
            Exits.Add(new Ship(this));
        }
    }

    internal class Ship : GenericRoom
    {
        public Ship(Harbor prev)
            : base("a single, solitary ship in the docks", "You can go here.", "ship", "solitary ship")
        {
            Exits.Add(prev);
        }

        public override void Describe(bool isFirstEntry = false)
        {
            Terminal.WriteLine(
                "As you go inside the ship, the ship's captain shoos you away, saying that he will meet you in the inn if you wish to talk.");
            Terminal.WriteLine("He then locks up the ship and walks away, whistling a jaunty tune.");
            ChangeRoom(new SecondHarbor());
        }
    }

    internal class SecondHarbor : GenericRoom
    {
        public SecondHarbor()
            : base(
                "the town's harbor", "You can go here.", "harbor", "the town's harbor", "town's harbor",
                "the towns harbor", "towns harbor")
        {

        }
    }

    internal class SecondSquare : GenericRoom
    {
        public SecondSquare(SecondHarbor prev)
            : base(
                "a road leading to the Town Square", "You can go here.", "town square",
                "road leading to the town square", "a road leading to the town square")
        {
            Exits.Add(prev);
            Exits.Add(new SecondInn());
        }
    }

    class SecondInn : GenericRoom
    {
        public SecondInn() : base("an inn", "The captain of the ship said to meet him here.", "inn")
        {
            
        }

        public override void Describe(bool isFirstEntry = false)
        {
            Terminal.WriteLine("As you walk into the inn, the captain of the ship jovially calls you to his table.");
            Terminal.WriteLine("You tell him your story, and how you wish to go to Buenos Aires to be with your beloved Lady Cunegonde");
            Terminal.WriteLine(
                "The captain regretfully informs you that he cannot aide you in your quest, as Lady Cunegonde is the favored consort of the governor of Buenos Aires, and he will have you hanged for trying to take her away.");
            Terminal.WriteLine(
                "Cacambo comes up with a brilliant compromise: He will spirit Lady Cunegonde away, and he will meet you in Venice with her.");
            Terminal.WriteLine("The captain finds you another ship, and you each set out upon your voyages.");
            Terminal.WriteLine(
                "However, much of your remaining wealth is stolen before you leave, leaving you with but a handful of jewels.");
            Terminal.Pause();
            Terminal.WriteLine(
                "Whilst on your journey to Venice, you meet a man named Martin, who claims to be the last Manichean in the world.");
            Terminal.WriteLine("in order to pass the time on your voyage, you engage in philosophical banter with Martin, who is adamantly convinced that God has abandoned the world to the control of a malevolent power");
            Terminal.WriteLine("On a whim, Martin and you decide to stop in Paris.");
            Terminal.Pause();
            Inventory.Clear();
            ChangeRoom(new ParisSquare());
        }
    }
}