using System.Collections.Generic;

namespace CandideTextAdventure
{
    internal class GenericItem : Item
    {
        public string description;
        public string name;

        public GenericItem(IEnumerable<string> identifiers, string name = "a generic item",
                           string description = "A generic item.")
        {
            ValidNames.AddRange(identifiers);
            this.name = name;
            this.description = description;
        }

        public GenericItem(string name = "a generic item", string description = "A generic item.",
                           params string[] identifiers)
            : this(identifiers, name, description)
        {
        }

        public override string GetName()
        {
            return name;
        }

        public override bool OnInteract(string command, string attemptedname)
        {
            if (command == "examine")
            {
                Terminal.WriteLine(description);
                return true;
            }
            return false;
        }
    }

    internal class GenericRoom : Room
    {
        public string desc;

        public GenericRoom(string name = "a generic room", string description = "You can go here.",
                           params string[] names)
        {
            propername = name;
            desc = description;
            Names.AddRange(names);
        }

        public override bool ExamineCommand()
        {
            Terminal.WriteLine(desc);
            return true;
        }
    }
}