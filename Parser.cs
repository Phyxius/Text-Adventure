using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CandideTextAdventure
{
    partial class Room
    {
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
                else if (split[0] == "inventory" || split[0] == "pack" || split[0] == "bag")
                {
                    if (Inventory.Count == 0)
                        Console.WriteLine("You have nothing but the clothes on your back.");
                    else if (Inventory.Count == 1)
                        Console.WriteLine("You have " + Inventory[0].GetName() + ".");
                    else
                    {
                        Console.Write("You have " + Inventory[0].GetName() + ", ");
                        for(int i = 1; i < Inventory.Count()-2; i++)
                            Console.Write(Inventory[i].GetName() + ", ");
                        Console.WriteLine("and " + Inventory[Inventory.Count()-1].GetName() + ".");
                    }
                    
                }
                else DisplayBadCommandError();
            }
            else if (split[0] == "go")
            {
                string target = "";
                if (split[1] == "to" || split[1] == "toward" && split.Count() > 2)
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
                foreach (Room r in CurrentRoom.Exits)
                    if (r.Names.Contains(target))
                    {
                        selected = r;
                        valid = true;
                        break;
                    }
                if (valid)
                    ChangeRoom(selected);
                else DisplayBadCommandError(ErrorType.InvalidLocation);
            }
            else if (split[0] == "pick")
            {
                string target = "";
                if (split[1] == "up" && split.Count() >= 3)
                {
                    for (int i = 2; i < split.Count(); i++)
                    {
                        target += split[i] + " ";
                    }

                }
                else
                {
                    for (int i = 1; i < split.Count(); i++)
                        target += split[i] + " ";
                }
                target = target.Substring(0, target.Length - 1);
                bool worked = false;
                foreach (Item i in CurrentRoom.Objects)
                {
                    if (i.ValidNames.Contains(target))
                    {
                        if (CurrentRoom.AttemptedPickup(i))
                            if (!i.AttemptedGrab())
                                DisplayBadCommandError(ErrorType.InvalidGrab);
                        worked = true;
                        break;
                    }
                }
                if (!worked)
                    DisplayBadCommandError(ErrorType.InvalidItem);
            }
            else if (split[0] == "grab" || split[0] == "take")
            {
                string target = "";
                for (int i = 1; i < split.Count(); i++)
                    target += split[i] + " ";
                target = target.Substring(0, target.Length - 1);
                bool worked = false;
                foreach (Item i in CurrentRoom.Objects)
                {
                    if (i.ValidNames.Contains(target))
                    {
                        if (CurrentRoom.AttemptedPickup(i))
                            if (!i.AttemptedGrab())
                                DisplayBadCommandError(ErrorType.InvalidGrab);
                        worked = true;
                        break;
                    }
                }
                if (!worked)
                    DisplayBadCommandError(ErrorType.InvalidItem);
            }
            else if (split[0] == "examine")
            {
                string target = "";
                for (int i = 1; i < split.Count(); i++)
                    target += split[i] + " ";
                target = target.Substring(0, target.Length - 1);
                bool worked = false;
            }
            else
            {
                string target = "";
                for (int i = 1; i < split.Count(); i++)
                    target += split[i] + " ";
                target = target.Substring(0, target.Length - 1);
                bool worked = false;
                foreach (Item i in CurrentRoom.Objects)
                {
                    if (i.ValidNames.Contains(target))
                    {
                        if (CurrentRoom.OnInteract(split[0], i))
                            if (!i.OnInteract(split[0]))
                                DisplayBadCommandError(ErrorType.InvalidUse);
                        worked = true;
                        break;
                    }
                }
                if (!worked)
                    DisplayBadCommandError(ErrorType.InvalidItem);
            }
        }
    }
}
