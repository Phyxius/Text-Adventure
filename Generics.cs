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

        public GenericItem(string name = "a generic item", string description = "A generic item.", params string[] identifiers)
            :this(identifiers,name,description)
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
			
			if(command =="help")
			{
				Terminal.WriteLine ("List of Commands:\n");
				Terminal.WriteLine ("Go {direction}: go in a direction on the map");
				Terminal.WriteLine ("Talk {name}: talk to a person");
				Terminal.WriteLine ("Take {item name}: pick up an item");
				Terminal.WriteLine ("Help: display command list");
				Terminal.WriteLine ("Inventory: display inventory items");
				Terminal.WriteLine ("Examine {object}: investigate an object in the room");
				//do for save and load
			}
            return false;
        }
    }
}
