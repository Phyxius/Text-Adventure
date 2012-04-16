using System;
using System.Collections.Generic;

namespace CandideTextAdventure
{
    internal class Item
    {
        public List<String> ValidNames = new List<string>(new String[] {});
        public string propername = "an Item";

        public virtual bool OnInteract(string command, string attemptedname)
        {
            return false;
        }

        public virtual string GetName()
        {
            return propername;
        }

        public virtual bool AttemptedGrab()
        {
            return false;
        }

        public virtual bool AttemptedSingleUse()
        {
            return false;
        }

        public virtual bool AttemptedDoubleItemUse(Item target)
        {
            return false;
        }
    }
}