using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CandideTextAdventure
{
    class GenericItem : Item
    {
        public readonly string name;
        public readonly string description;
        public GenericItem(IEnumerable<string> identifiers, string name = "a generic item", string description = "A generic item.")
        {
            ValidNames.AddRange(identifiers);
            this.name = name;
            this.description = description;
        }

        public override string GetName()
        {
            return name;
        }

        public override bool OnInteract(string command)
        {
            if (command == "examine")
            {
                Console.WriteLine(description);
                return true;
            }
            return false;
        }
    }
}
