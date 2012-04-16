using CandideTextAdventure.Chapter19;

namespace CandideTextAdventure.Chapter16
{
    internal class Chapter16Start : GenericRoom
    {
        public Chapter16Start()
            : base("a generic room", "You can go here.")
        {
        }

        public override void OnEnter()
        {
            MusicSystem.MusicSystem.ChangeSong("john williams.ogg");
            Terminal.WriteLine(
                "After killing your beloved Lady Cunegonde's Brother, you flee to the jungles of Paraguay with your trusty servant Cacambo.");
            Terminal.WriteLine(
                "You wander for weeks, narrowly avoiding starvation and being eaten by vicious beasts and barbarous tribes of primitive men.");
            Terminal.WriteLine(
                "Eventually you stumble across a road. As you begin to follow it, the paving stones steadily transition from stone into pure gold. Rubies and other gems begin to be seen simply lying on the ground.");
            Terminal.WriteLine(
                "Cacambo begins to collect them, until he fills a huge sack, which he gives to you to carry.");
            Inventory.Add(new GenericItem("a sack full of gems",
                                          "This sack is filled to the brim with priceless treasures.", "sack",
                                          "sack filled with gems", "sack full of gems"));
            Terminal.Pause();
            Terminal.WriteLine(
                "Eventually, you come upon a villiage, where you can hear a large crowd inside a building.");
            Terminal.WriteLine("Cacambo goes to investigate. He listens at the door for a bit.");
            Terminal.WriteLine(
                "When he returns, he exclaims, \"They are speaking Portuguese, my native tongue! I will act as your interpreter. That is an inn, we can go in and get some food.\"");
            ChangeRoom(new ElDoradoEntrance());
        }
    }

    internal class ElDoradoEntrance : GenericRoom
    {
        public ElDoradoEntrance() : base("a door leading outside", "You can go here.", "outside")
        {
            Items.Add(new Monument());
            Exits.Add(new Inn2(this));
        }
    }

    internal class RoadSign2 : GenericItem
    {
        public RoadSign2()
            : base("a road sign", "This road sign reads, \"Welcome to El Dorado, City of Gold!\"", "sign", "road sign")
        {
        }
    }

    internal class Monument : GenericItem
    {
        public Monument()
            : base(
                "a small skull made of crystal on a small plinth",
                "This skull is made of pure crystal. A plaque next to it reads, \"This is a monument to the Interdimensional Beings who helped us build our city.\"\nScrawled on the side of the plinth in English is a message reading, \"Indy was here!\"",
                "monument", "skull", "crystal skull")
        {
        }
    }

    internal class Inn2 : GenericRoom
    {
        public Inn2(Room prev)
            : base(
                "an inn", "You can hear rowdy laughter and unintelligible speach emanating from inside.", "inn",
                "inside", "inside inn")
        {
            Items.Add(new PlateAgain());
            Exits.Add(prev);
        }

        public override void Describe(bool isFirstEntry = false)
        {
            Terminal.WriteLine("You order a delicious meal.");
            base.Describe(isFirstEntry);
        }

        public override bool AttemptedExit(Room target)
        {
            if (target.GetType() == typeof (ElDoradoIntermission))
                return true;
            Terminal.WriteLine("Wait! You haven't finished your dinner!");
            return false;
        }
    }

    internal class PlateAgain : GenericItem
    {
        public PlateAgain()
            : base(
                new[] {"plate", "dinnerplate", "plate of food", "dinnerplate full of food", "food", "meal", "dinner"},
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
                    Room.CurrentRoom.Items.Remove(this);
                    Terminal.RestartFromLastCheckpoint<ElDoradoEntrance>();
                    return true;
                }
                Room.ChangeRoom(new ElDoradoIntermission());
                return true;
            }
            return base.OnInteract(command, attemptedname);
        }
    }

    internal class ElDoradoIntermission : Room
    {
        public override void OnEnter()
        {
            Terminal.WriteLine("You eat your meal. It is delicious.");
            Terminal.WriteLine("As you get up, you try to pay with some of your gems.");
            Terminal.WriteLine(
                "The waitress refuses it, saying in portuguese (as translated by Cacambo), \"You do not need to pay here. All inns are funded by the government for the good of the people.\"");
            Terminal.WriteLine(
                "The waitress then tells you that you are in the city of El Dorado, and that since you are obviously strangers, you should go West to the palace so the King may properly welcome you.");
            Terminal.Pause();
            Terminal.WriteLine(
                "You travel West with Cacambo until you see the road turn towards the North, where you see the entrance to the Palace.");
            ChangeRoom(new PalaceRoad());
        }
    }

    internal class PalaceRoad : GenericRoom
    {
        public PalaceRoad() : base("a road", "You can go here.", "road", "south")
        {
            Exits.Add(new PalaceFoyer());
        }
    }

    internal class PalaceFoyer : GenericRoom
    {
        public PalaceFoyer()
            : base(
                "the entrance to the Palace", "You can go here.", "palace", "entrance", "entrance to the palace",
                "the entrance", "the entrance to the palace")
        {
            Items.Add(new KingStatue());
            Items.Add(new SignForTheUmpteenthTime());
            Exits.Add(new Palace());
        }

        public override void Describe(bool isFirstEntry = false)
        {
            Terminal.WriteLine(
                "When you walk into the palace's foyer, a guard approaches you and says, \"The King of El Dorado would like to see you whenever you are ready.\"");
            Terminal.WriteLine("The guard then walks back to his post outside that palace.");
            base.Describe(isFirstEntry);
        }
    }

    internal class KingStatue : GenericItem
    {
        public KingStatue()
            : base(
                "a large golden statue",
                "This statue depits a man with a crown on his head, presumably the King. An inscription on a plaque on the bottom reads, \"Welcome to El Dorado, City of Gold! This city was fabled by the Spaniards and sought out by many search parties, but never found.\"",
                "statue", "golden statue", "large golden statue", "a large golden statue", "large statue", "gold statue",
                "large gold statue")
        {
        }
    }

    internal class SignForTheUmpteenthTime : GenericItem
    {
        public SignForTheUmpteenthTime()
            : base(
                "a sign",
                "This sign reads, \"The Spanish have conquered most of Latin America, but thankfully not our city. Their biggest two reasons for conquering our neighbors is an attempt to spread their religion, and to loot our riches.\"",
                "sign", "a sign")

        {
        }
    }

    internal class Palace : GenericRoom
    {
        public Palace()
            : base(
                "the entrance to the King's Audience Room", "You can go here.", "palace", "entrance",
                "entrance to the audience room", "the audience room", "the entrance to the audience room",
                "audience room")
        {
        }

        public override void OnEnter()
        {
            Terminal.WriteLine(
                "You enter the King's Audience Room and meet with the king, who tells you all about this strange city.");
            Terminal.WriteLine(
                "Eventually, he gives you quarters in the palace, where you stay for some time, enjoying the comforts of the city.");
            Terminal.Pause();
            Terminal.WriteLine("Some weeks (or months, you lost track) later, you grow bored and restless.");
            Terminal.WriteLine(
                "You eventually realize the source of your discomfort: Lady Cunegonde. She is not here with you.");
            Terminal.WriteLine(
                "You go to the king and tell him this. He has his best engineers design for you a device with which to leave his secluded city.");
            Terminal.Pause();
            Terminal.WriteLine(
                "When the device is completed, the King gives you a flock of red sheep laden with gold and diamonds to help you on your journey.");
            Terminal.WriteLine(
                "You use the device and again wander through the jungle. You eventually come to a city, after losing all but a few of your sheep.");
            Terminal.Pause();
            Terminal.WriteLine("You decide to find a ship to deliver you to Buenos Aires.");
            ChangeRoom(new Chapter19Start());
        }
    }
}