using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CandideTextAdventure
{
    class Room
    {
        public static Room CurrentRoom = new Room();
        public static String[] Errors = new string[]{"Now you're just talking nonsense!", "What?"};
        public static void ParseInput(string input)
        {
            var split = input.ToLower().Split(' ');
            if (split.Count() == 1)
            {
                if (split[0] == "examine" || split[0] == "look")
                {
                    if (CurrentRoom.OnExamine())
                    {
                        ListItems(CurrentRoom);
                        ListExits(CurrentRoom);
                    }
                }
                else DisplayBadCommandError();
            }
            else if (split[0] == "go")
            {
                string target = "";
                if (split[1] == "to" && split.Count() > 2)
                    for (int i = 2; i < split.Count(); i++)
                        target += split[i] + " ";
                else
                {
                    for (int i = 1; i < split.Count(); i++)
                        target += split[i] + " ";
                }
                target = target.Substring(0, target.Length - 1);
                bool valid = false;
                Room selected = new Room();
                foreach(Room r in CurrentRoom.Exits)
                    if (r.Names.Contains(target))
                    {
                        selected = r;
                        valid = true;
                        break;
                    }
                if (valid)
                    ChangeRoom(selected);
                else DisplayBadCommandError();
            }
            else
            {
                string target = "";
                for (int i = 1; i < split.Count(); i++ )
                    target += split[i] + " ";
                target = target.Substring(0, target.Length - 1);
                bool worked = false;
                foreach (Item i in CurrentRoom.Objects)
                {
                    if (i.ValidNames.Contains(target))
                    {
                        if (CurrentRoom.OnInteract(split[0], i))
                            i.OnInteract(split[0]);
                        worked = true;
                        break;
                    }
                }
                if (!worked)
                    DisplayBadCommandError();
            }
        }
        public static void ChangeRoom(Room newRoom)
        {
            CurrentRoom = newRoom;
            if (newRoom.OnExamine() && newRoom.Objects.Count > 0)
                ListItems(newRoom);
            if (newRoom.Exits.Count > 0)
                ListExits(newRoom);
            newRoom.OnEnter();

        }
        public static void ListItems(Room room)
        {
            if (room.Objects.Count == 0)
                return;
            Console.Write("You see ");
            if (room.Objects.Count > 1)
            {
                for (int i = 0; i < room.Objects.Count - 1; i++)
                    Console.Write(room.Objects[i].GetName() + ", ");
                Console.WriteLine("and " + room.Objects[room.Objects.Count - 1].GetName() + ".");
            }
            else Console.WriteLine(room.Objects[0].GetName() + ".");

        }
        public static void ListExits(Room room)
        {
            if (room.Exits.Count == 0)
                return;
            Console.Write("You see ");
            if (room.Exits.Count > 1)
            {
                for (int i = 0; i < room.Exits.Count - 1; i++)
                    Console.Write(room.Exits[i].GetName() + ", ");
                Console.WriteLine("and " + room.Exits[room.Exits.Count - 1] + ".");
            }
            else Console.WriteLine(room.Exits[0].GetName() + ".");

        }
        static void DisplayBadCommandError()
        {
            Console.WriteLine(Errors[new Random().Next(Errors.Count())]);
        }

        public List<Item> Objects = new List<Item>();
        public List<Room> Exits = new List<Room>();
        public List<string> Names = new List<string>();
        public virtual bool OnInteract(string command, Item target)
        {
            return true;
        }
        public virtual bool OnExamine()
        {
            Console.WriteLine("Just an empty dev room. How did you get here?");
            return true;
        }
        public virtual string GetName()
        {
            return "a door to a dev room";
        }
        public virtual void OnEnter()
        {
            return;
        }
        public virtual void OnExit()
        {
            return;
        }

    }
}
