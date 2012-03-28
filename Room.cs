using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CandideTextAdventure
{
	partial class Room
	{
		public List<Item> Items = new List<Item>();
		public List<Room> Exits = new List<Room>();
		public List<string> Names = new List<string>();
	    public string propername = "a door to a dev room";
		public virtual bool OnInteract(string command, Item target)
		{
			return true;
		}
		public virtual bool OnExamine()
		{
			//Terminal.WriteLine("Just an empty dev room. How did you get here?");
			return true;
		}
		public virtual string GetName()
		{
			return propername;
		}
		public virtual void OnEnter()
		{
			return;
		}
		public virtual void OnExit()
		{
			return;
		}
		public virtual void Describe(bool isFirstEntry = false)
		{
			if (this.OnExamine() && this.Items.Count > 0)
				ListItems(this);
			if (this.Exits.Count > 0)
				ListExits(this);
			this.OnEnter();
		}

		public virtual bool AttemptedExit(Room target)
		{
			return true;
		}

		public virtual bool AttemptedPickup(Item target)
		{
			return true;
		}
		
		public virtual bool AttemptedSingleItemUse(Item target)
		{
			return true;
		}
		
		public virtual bool AttemptedDoubleItemUse(Item source, Item target)
		{
			return true;
		}

	}
}
