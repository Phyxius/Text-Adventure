using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                {
                    foreach (Item i in Inventory)
                    {
                        if (i.ValidNames.Contains(target))
                        {
                            worked = true;
                            i.OnInteract(split[0]);
                            break;
                        }
                    }
                }
                
                if (!worked)
                    DisplayBadCommandError(ErrorType.InvalidItem);
            }
            else if (split[0] == "use")
            {
                /*if (split.Count() > 2)
                {
                    if (split[2] == "on")
                    {
                        if (split.Count() < 4)
                        {
                            DisplayBadCommandError(ErrorType.General)
                            return;
                        }
                        Item source;
                        bool targetExists = false;
                        foreach(Item i in Inventory)
                        {
                            if (i.ValidNames.Contains(split[1]))
                            {
                                source = i;
                                targetExists = true;
                                break;
                            }
                        }
                        if (!targetExists)
                        {
                            DisplayBadCommandError(ErrorType.InvalidItem)
                            return;
                        }
                        string target = "";
                        for(int i = 3; i < split.Count(); i++)
                        {
                            target += split[i];
                            if (i < split.Count() - 1)
                                target += " ";
                        }
                        bool worked = false;
                        foreach (Item i in Inventory)
                        {
                            if (i.ValidNames.Contains(target))
                            {
                                if (CurrentRoom.AttemptedSingleItemUse(i))
                                    if (
                            }
                        }
                    }
                }*/
                if (!split.Contains("on"))
                {
                    string target = "";
                    for(int i = 1; i < split.Count(); i++)
                    {
                        target += split[i];
                        if (i < split.Count() - 1)
                            target += " ";
                    }
                    bool worked = false;
                    foreach (Item i in Inventory)
                    {
                        if (!i.ValidNames.Contains(target))
                            continue;
                        if (CurrentRoom.AttemptedSingleItemUse(i))
                            if (i.AttemptedSingleUse())
                                worked = true;
                        break;
                    }
                    if (!worked)
                        DisplayBadCommandError(ErrorType.InvalidSingleUse);
                }
                else
                {
                    if (split.Count() < 4)
                    {
                        DisplayBadCommandError();
                        return;
                    }
                    string source = "";
                    int onIndex = 0;
                    string target = "";
                    for (int i = 1; i < split.Count(); i++)
                    {
                        if (split[i] == "on")
                            break;
                        source += split[i];
                        source += " ";
                        onIndex = i + 1;
                    }
                    source = source.Substring(0, source.Length - 1);
                    for (int i = onIndex + 1; i < split.Count(); i++)
                    {
                        target += split[i];
                        if (i < split.Count() - 1)
                            target += " ";
                    }
                    bool sourceworked = false;
                    Item sourceitem = null;
                    foreach (Item i in Inventory)
                        if (i.ValidNames.Contains(source))
                        {
                            sourceitem = i;
                            sourceworked = true;
                            break;
                        }
                    if (!sourceworked)
                    {
                        DisplayBadCommandError(ErrorType.InvalidItem);
                        return;
                    }
                    bool targetworked = false;
                    Item targetitem = null; 
                    foreach(Item i in Inventory)
                        if (i.ValidNames.Contains(target))
                        {
                            targetitem = i;
                            targetworked = true;
                            break;
                        }
                    if (!targetworked)
                        foreach (Item i in CurrentRoom.Objects)
                            if (i.ValidNames.Contains(target))
                            {
                                targetitem = i;
                                targetworked = true;
                                break;
                            }
                    if (!targetworked)
                    {
                        DisplayBadCommandError(ErrorType.InvalidItem);
                        return;
                    }
                    Debug.Assert(sourceitem != null);
                    Debug.Assert(targetitem != null);
                    if (!((CurrentRoom.AttemptedDoubleItemUse(sourceitem, targetitem) && sourceitem.AttemptedDoubleItemUse(targetitem))))
                        DisplayBadCommandError(ErrorType.InvalidUse);
                }
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
