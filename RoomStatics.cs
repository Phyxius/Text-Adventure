using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CandideTextAdventure
{
    partial class Room
    {
        public static Room CurrentRoom = new Room();
        public static void ChangeRoom(Room newRoom)
        {
            if (!CurrentRoom.AttemptedExit())
                return;
            CurrentRoom = newRoom;
            newRoom.Describe(true);
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
        static void DisplayBadCommandError(ErrorType type = ErrorType.General)
        {
            string[] potentials;
            switch (type)
            {
                case ErrorType.InvalidGrab:
                    potentials = new[] {"I can't take that!", "How would I do that?"};
                    break;
                case ErrorType.InvalidItem:
                    potentials = new[] {"What is that?", "I don't see that"};
                    break;
                case ErrorType.InvalidLocation:
                    potentials = new string[] {"Where?", "I can't go there!"};
                    break;
                case ErrorType.InvalidUse:
                    potentials = new string[]
                                     {"How am I supposed to do that?", "I'm sorry dave, I'm afraid I can't do that."};
                    break;
                case ErrorType.InvalidSingleUse:
                    potentials = new string[]{"On what?"};
                    break;
                case ErrorType.General:
                default:
                    potentials = new string[] {"Now you're just talking nonsense!", "What?"};
                    break;
            }
            Console.WriteLine(potentials[new Random().Next(potentials.Count())]);
        }

        enum ErrorType
        {
            InvalidUse, InvalidLocation, General, InvalidItem, InvalidGrab, InvalidSingleUse
        }
    }
}
