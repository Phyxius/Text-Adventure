using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CandideTextAdventure.Chapter16;

namespace CandideTextAdventure.Chapter11
{
    internal class Chapter11Start : GenericRoom
    {
        public Chapter11Start()
            : base("a room", "you can go here")
        {

        }

        public override void OnEnter()
        {
            Terminal.WriteLine(
                "On your voyage, the old woman reveals herself to be the daughter of Pope Urban X, and the Princess of Palestria.");
            Terminal.WriteLine(
                "Naturally, Cunegonde and you are both astonished at the knowledge that the Pope had a daughter, but the old woman burshes the information off as if it were nothing.");
            Terminal.WriteLine(
                "The old woman continues to tell you of her adventures, ending in how her left buttock was cut off to serve as a meal for some Janizaries.");
            Terminal.Pause();
            MusicSystem.MusicSystem.ChangeSong("Light Cavalry.ogg");
            Terminal.WriteLine(
                "When you reach Buenos Aires, you hear tell that a ship has come from Lisbon in search of the murderer of the Grand Inquisitor, namely yourself, and that within the hour of you disembarking on the soil of Buenos Aires, you will be hanged.");
            Terminal.WriteLine(
                "You flee to take refuge in the army of the Jesuits of Paraguay, whom you were originally sent to fight. Your bring with you your trusty servant Cacambo, whom you picked up on your voyage.");
            Terminal.Pause();
            Terminal.WriteLine(
                "You are taken to the leader of the Jesuits, who tells you that he will not speak to any Spaniards, and that you should therefore leave.");
            Terminal.WriteLine("He is happy to speak to you when he realizes you are not Spanish, but German, however.");
            Terminal.WriteLine(
                "As you speak with him, you recognize him as Lady Cunegonde's brother, and he recognizes you as well.");
            Terminal.WriteLine(
                "He then invites you to rest in his guest room, and to join him for dinner in one hour's time.");
            Terminal.Pause();
            ChangeRoom(new Chapter15Start());
        }
    }

    internal class Chapter15Start : GenericRoom
    {
        private bool spoken = false;

        public Chapter15Start()
            : base(
                "a door to the guest bedroom", "You can go here.", "guest bedroom", "bedroom", "guest room",
                "door to the guest bedroom", "door to bedroom", "door to guest room")
        {
            Items.AddRange(new Item[] {new SecondBed(), new SwordPlaque(), new MonaLisa(),});
            Exits.Add(new Chapter15Hallway(this));
        }

        public override void Describe(bool isFirstEntry = false)
        {
            if (MusicSystem.MusicSystem.LastSong != "Light Cavalry.ogg")
                MusicSystem.MusicSystem.ChangeSong("Light Cavalry.ogg");
            if (!spoken)
            {
                spoken = true;
                Terminal.WriteLine(
                    "After napping for an hour, you decide it is time to join Cunegonde's brother and Cacambo for dinner.");
            }
            base.Describe(isFirstEntry);
        }
    }

    internal class SecondBed : GenericItem
    {
        public SecondBed()
            : base(
                "a bed", "This bed is also not very comfortable. It seems there are no comfortable beds in the world.",
                "bed", "the bed")
        {

        }
    }

    internal class SwordPlaque : GenericItem
    {
        public bool examined = false;

        public SwordPlaque()
            : base(
                "a sword on a plaque", "The plaque reads, \"Excalibur, weapon of Kings.\"", "sword", "plaque",
                "sword on plaque", "excalibur", "excalibur on a plaque")
        {

        }

        public override bool OnInteract(string command, string attemptedname)
        {
            if (attemptedname.ToLower().Contains("excalibur") && !examined)
            {
                Terminal.WriteLine("Cheating, are we? You're not supposed to know that yet!");
                return true;
            }
            if (command == "examine")
            {
                examined = true;
                propername = "Excalibur on its plaque";
            }
            return base.OnInteract(command, attemptedname);
        }

        public override bool AttemptedGrab()
        {
            Terminal.WriteLine("I can't! It's stuck to the wall!");
            return true;
        }
    }

    internal class MonaLisa : GenericItem
    {
        public MonaLisa()
            : base(
                "a painting of a woman",
                "The woman in the painting looks Italian. She is smiling as though she knows something that you do not. Much of the artist's signature has rubbed off, but you can make out a few letters: \"-inci\".",
                "painting", "painting of woman", "painting of a woman", "woman")
        {

        }

        public override bool AttemptedGrab()
        {
            Terminal.WriteLine("You're no art thief!");
            return true;
        }

        public override bool OnInteract(string command, string attemptedname)
        {
            if (command == "stab" || command == "destroy" || command == "hit")
            {
                if (Room.Inventory.Count > 0)
                {
                    Terminal.WriteLine("You destroy the Mona Lisa.");
                    Terminal.WriteLine(
                        "You feel a great disturbance in the Force, as if millions of art critics cried out in terror, and were suddenly silenced.");
                    Room.CurrentRoom.Items.Remove(this);
                    return true;
                }
                Terminal.WriteLine("With what?");
                return true;
            }
            return base.OnInteract(command, attemptedname);
        }
    }

    internal class Excalibur : GenericItem
    {
        private bool examined;

        public Excalibur(bool known = false)
            : base(
                known ? "Excalibur" : "a sword lying on the ground",
                "This sword has an inscription on on it. It reads, \"Excalibur, weapon of Kings.\"", "sword",
                "excalibur", "sword on the ground")
        {
            examined = known;
        }

        public override bool OnInteract(string command, string attemptedname)
        {
            if (attemptedname.ToLower().Contains("excalibur") && !examined)
            {
                Terminal.WriteLine("Cheating, are we? You're not supposed to know that yet!");
                return true;
            }
            if (command == "examine")
                examined = true;
            return base.OnInteract(command, attemptedname);
        }

        public override bool AttemptedDoubleItemUse(Item target)
        {
            if (target.GetType() == typeof (MonaLisa))
            {
                Terminal.WriteLine("You stab the Mona Lisa, destroying it.");
                Terminal.WriteLine(
                    "You feel a great disturbance in the Force, as if millions of art critics cried out in terror, and were suddenly silenced.");
                Room.CurrentRoom.Items.Remove(target);
                return true;
            }
            if (target.GetType() == typeof(CunegondesBrother))
            {
                Room.ParseInput("stab brother");
                return true;
            }
            return false;
        }

        public override bool AttemptedGrab()
        {
            Terminal.WriteLine("You pick up " + (examined ? "Excalibur " : "the sword ") + "and wear it on your hip.");
            Room.Inventory.Add(this);
            Room.CurrentRoom.Items.Remove(this);
            return true;
        }
    }

    internal class Chapter15Hallway : GenericRoom
    {
        private Chapter15Start PrevRoom;

        public Chapter15Hallway(Chapter15Start prev)
            : base("a door to a hallway", "You can go here.", "hallway", "door to hallway", "door to a hallway")
        {
            PrevRoom = prev;
            Exits.Add(prev);
            Exits.Add(new DinnerRoom(this));
            Items.Add(new Crowbar());
        }

    }

    internal class Crowbar : GenericItem
    {
        public Crowbar()
            : base(
                "a crowbar lying on the ground", "Some crudely carved letters on the crowbar read, \"Property of G. Freeman. If found, return to Black Mesa Research Facility, Black Mesa, New Mexico.\"", "crowbar", "crowbar lying on the ground",
                "crowbar on the ground")
        {
            propername = name;
        }

        public override bool AttemptedGrab()
        {
            Terminal.WriteLine("You pick it up.");
            propername = "a crowbar";
            Room.Inventory.Add(this);
            Room.CurrentRoom.Items.Remove(this);
            return true;
        }

        public override string GetName()
        {
            return propername;
        }

        public override bool AttemptedDoubleItemUse(Item target)
        {
            if (target.GetType() == typeof (MonaLisa))
            {
                Terminal.WriteLine("You're no art theif!");
                return true;
            }
            if (target.GetType() == typeof (SwordPlaque))
            {
                Terminal.WriteLine(
                    "You pry the sword off of the plaque. It falls to the ground with a loud clanking sound.");
                Terminal.WriteLine("Your crowbar breaks.");
                Room.CurrentRoom.Items.Add(new Excalibur());
                Room.CurrentRoom.Items.Remove(target);
                Room.Inventory.Remove(this);
                return true;
            }
            return false;
        }
    }

    internal class DinnerRoom : GenericRoom
    {
        private CunegondesBrother brother;
        public DinnerRoom(Chapter15Hallway prev)
            : base(
                "the door to the dining room", "You can go here.", "dining room", "the door to the dining room",
                "door to the dining room")
        {
            Exits.Add(prev);
            Exits.Add(new Chapter15Outside());
            brother = new CunegondesBrother();
            Items.Add(new CunegondesBrother());
        }

        public override void Describe(bool isFirstEntry = false)
        {
            if (Inventory.Count > 0 && Inventory[0].GetType() == typeof(Crowbar))
            {
                Inventory.Clear();
                Terminal.WriteLine(
                    "As you walk into the dining room, Cunegonde's brother notices your crowbar. He takes it from you and thanks you for finding it for him.");
            }
            Terminal.WriteLine("You eat dinner with Cunegonde's brother and Cacambo.");
            Terminal.WriteLine(
                "After dinner, Cunegonde's brother dismisses Cacambo to his room and invites you chat with him.");
            Terminal.WriteLine("You tell him of your plans to marry his sister, Cunegonde.");
            Terminal.WriteLine(
                "At this, he becomes mad with rage and swears he would rather kill you than allow you to marry her.");
            base.Describe();
        }

        public override bool AttemptedExit(Room target)
        {
            if(target.GetType() == typeof(Chapter16.Chapter16Start))
                return true;
            if(Items.Contains(brother))
                return true;
            Terminal.WriteLine("As you attempt to leave, Cunegonde's borther stabs you dead.");
            if(Inventory.Count == 0)
                Terminal.WriteLine("If only you had a sword! Then you could kill him before he killed you.");
            Terminal.RestartFromLastCheckpoint<Chapter15Start>();
            return false;
        }
    }

    internal class Chapter15Outside : GenericRoom
    {
        public Chapter15Outside()
            : base(
                "a door leading outside", "You can see light streaming in from outside.", "outside",
                "door leading outside")
        {

        }
    }

    internal class CunegondesBrother : GenericItem
    {
        public CunegondesBrother()
            : base(
                "Cunegonde's brother",
                "This is Cunegonde's brother. He looks very angry.", "cunegonde's brother", "cunegondes brother", "brother")
        {
            var tmp = new List<string>();
            foreach (string s in ValidNames)
            {
                tmp.Add("with " + s);
                tmp.Add("to " + s);

            }
            ValidNames.AddRange(tmp);
        }

        public override bool OnInteract(string command, string attemptedname)
        {
            switch(command)
            {
                case "kill":
                case "stab":
                case"murder":
                    if (Room.Inventory.Count > 0 && Room.Inventory[0].GetType() == typeof(Excalibur))
                    {
                        Terminal.WriteLine("You kill Cunegonde's brother.");
                        Terminal.Pause();
                        Room.CurrentRoom.Items.Remove(this);
                        Room.ChangeRoom(new Chapter16Start());
                    }
                    else
                    {
                        Terminal.WriteLine("With what? Some bad words?");
                    }
                    return true;
                case "speak":
                case "talk":
                    Terminal.WriteLine("Cunegonde's brother is not interested in talking. He stabs you dead.");

                    Terminal.RestartFromLastCheckpoint<Chapter15Start>();
                    return true;
            }
            return base.OnInteract(command, attemptedname);
        }
    }

}

