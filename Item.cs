using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CandideTextAdventure
{
    class Item
    {
        public List<String> ValidNames = new List<string>(new String[]{});
        public virtual bool OnInteract(string command)
        {
            return false;
        }
        public virtual string GetName()
        {
            return "an Item";
        }
        public virtual bool AttemptedGrab()
        {
            return false;
        }

    }
}
