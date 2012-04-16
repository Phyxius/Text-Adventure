namespace CandideTextAdventure.Chapter22
{
    internal class ParisSquare : GenericRoom
    {
        private bool played;

        public ParisSquare()
            : base(
                "a door to the city square", "You can go here.", "square", "city square", "the city square", "out",
                "outside", "city")
        {
            Items.Add(new ParisSign());
            var tmp = new ParisInn(this);
            Exits.Add(tmp);
            Exits.Add(new ParisAbbey(tmp.bed, this));
            Inventory.Add(new FewGems());
            Items.Add(new StrangeStone());
        }

        public override void Describe(bool isFirstEntry = false)
        {
            if (!played)
                MusicSystem.MusicSystem.ChangeSong("Crusell_Clarinet_concerto_No.ogg");
            played = true;
            base.Describe(isFirstEntry);
        }
    }

    internal class FewGems : GenericItem
    {
        public FewGems()
            : base(
                "a handful of diamonds and rubies", "This is all that remains of your great wealth from El Dorado.",
                "gems", "rubies", "diamonds", "diamonds and rubies", "a handful of diamonds and rubies")
        {
        }
    }

    internal class ParisSign : GenericItem
    {
        public ParisSign() : base("a sign", "This sign says, \"Welcome to Paris!\"", "sign", "a sign")
        {
        }
    }

    internal class StrangeStone : GenericItem
    {
        public StrangeStone()
            : base(
                "a strange stone",
                "This stone looks very strange. You can faintly see the inscription, \"Property of N. Flamel.\"",
                "stone", "strange stone")
        {
        }

        public override bool AttemptedGrab()
        {
            Room.CurrentRoom.Items.Remove(this);
            Terminal.WriteLine(
                "As you reach for the stone, another man appears and takes it out from under your nose. He walks away, saying over his shoulder, \"Can't have a Philosopher's Stone winding up in the wrong hands, can we?\"");
            return true;
        }
    }

    internal class ParisInn : Room
    {
        public ParisBed bed;
        private bool entered;

        public ParisInn(ParisSquare prev)
        {
            Exits.Add(prev);
            propername = "an inn";
            bed = new ParisBed();
            Items.Add(bed);
            Items.Add(new GenericItem("a chair", "This chair isn't very comfortable.", "chair", "a chair"));
            Names.AddRange(new[] {"inn", "an inn", "in"});
        }

        public override void Describe(bool isFirstEntry = false)
        {
            if (!entered)
            {
                entered = true;
                Terminal.WriteLine("You rent a room at the inn.");
            }
            Terminal.WriteLine("You go up to your room.");
            base.Describe(isFirstEntry);
        }
    }

    internal class ParisBed : GenericItem
    {
        public bool Sleepable; // = false;

        public ParisBed()
            : base(
                "a bed", "This bed is not very comfortable. Why are there no comfortable beds in the world?", "bed",
                "a bed", "in bed")
        {
        }

        public override bool OnInteract(string command, string attemptedname)
        {
            if (command == "sleep" || command == "use")
            {
                if (!Sleepable)
                    Terminal.WriteLine("You aren't tired yet!");
                else
                {
                    Terminal.WriteLine("You go to sleep extremely excitedly, waking up the next day.");
                    ParisAbbey.hasSlept = true;
                    Sleepable = false;
                }
                return true;
            }
            return base.OnInteract(command, attemptedname);
        }
    }

    internal class ParisAbbey : GenericRoom
    {
        public static bool hasSlept;
        private readonly ParisBed bed;
        public bool HasEntered;

        public ParisAbbey(ParisBed b, ParisSquare prev)
            : base("a small abbey", "You can go here.", "abbey", "small abbey", "a small abbey", "the small abbey")
        {
            bed = b;
            Exits.Add(prev);
            Items.Add(new AbbeysOwner());
        }

        public override void Describe(bool isFirstEntry = false)
        {
            if (hasSlept)
            {
                Terminal.WriteLine(
                    "The abbey's owner escorts you to a pitch-black room, saying that the light is bad for your Lady Cunegonde's health.");
                Terminal.WriteLine(
                    "You see the faint outline of a woman in the room, and you spend some time simply sitting by her side. After, you leave a bag full of your jewels and money with her as a gift.");
                Inventory.Clear();
                Terminal.WriteLine(
                    "As you leave, you are stopped by the police, who arrest you for being 'suspicious strangers'.");
                Terminal.WriteLine("You bribe them with the last of your gems, and run off with Martin back to sea.");
                Terminal.Pause();
                Terminal.WriteLine(
                    "When you arrive in Venice, you meet up again with Paquette and Cacambo. Cacambo tells you that Cunegonde is in Constantinople.");
                Terminal.WriteLine("You set off for Constantinople at once.");
                Terminal.Pause();
                Terminal.WriteLine(
                    "When you arrive in Constantinople, you find Lady Cunegonde, Dr. Pangloss, and the Baron of Thunder-ten-tronkch.");
                Terminal.WriteLine(
                    "You announce to the Baron your intent to marry Cunegonde, and he does not take it very well. Rather than add another murder to your conscience, you sell him into slavery, marry Cunegonde, and buy a small farm in Turkey for everyone to live on.");
                Terminal.WriteLine("And everyone lived happily ever after!");
                Terminal.Pause();
            }
            else if (!HasEntered)
            {
                Terminal.WriteLine(
                    "You strike up a conversation with the owner of the Abbey. He seems very interested in the tales of your adventures, especially hearing how you came to Paris with a pocket full of gems.");
                Terminal.WriteLine(
                    "After you are done telling your story, the Abbey tells you that he has Lady Cunegonde in the abbey, but that she is very sick. He says you can see her tomorrow.");
                Terminal.WriteLine("Ecstatic at hearing this, you decide to go to sleep as soon as possible.");
                HasEntered = true;
                bed.Sleepable = true;
            }
            base.Describe(isFirstEntry);
        }
    }

    internal class AbbeysOwner : GenericItem
    {
        public AbbeysOwner()
            : base(
                "the owner of the abbey", "He looks extremely pious.", "abbey", "abbey's owner", "owner of the abbey",
                "the owner of the abbey", "abbys owner", "owner")
        {
        }
    }
}