using System;
using System.Linq;

namespace CandideTextAdventure
{
    internal partial class Room
    {
        public static Room CurrentRoom = new Room();

        public static void ChangeRoom(Room newRoom, bool force = false)
        {
            if (!force && !CurrentRoom.AttemptedExit(newRoom))
                return;
            CurrentRoom = newRoom;
            newRoom.Describe(true);
        }

        public static void ListItems(Room room)
        {
            if (room.Items.Count == 0)
                return;
            Terminal.Write("You see ");
            if (room.Items.Count > 1)
            {
                for (int i = 0; i < room.Items.Count - 1; i++)
                    Terminal.Write(room.Items[i].GetName() + ", ");
                Terminal.WriteLine("and " + room.Items[room.Items.Count - 1].GetName() + ".");
            }
            else Terminal.WriteLine(room.Items[0].GetName() + ".");
        }

        public static void ListExits(Room room)
        {
            if (room.Exits.Count == 0)
                return;
            Terminal.Write("You see ");
            if (room.Exits.Count > 1)
            {
                for (int i = 0; i < room.Exits.Count - 1; i++)
                    Terminal.Write(room.Exits[i].GetName() + ", ");
                Terminal.WriteLine("and " + room.Exits[room.Exits.Count - 1].GetName() + ".");
            }
            else Terminal.WriteLine(room.Exits[0].GetName() + ".");
        }

        private static void DisplayBadCommandError(ErrorType type = ErrorType.General)
        {
            string[] potentials;
            switch (type)
            {
                case ErrorType.InvalidGrab:
                    potentials = new[] {"I can't take that!", "How would I do that?"};
                    break;
                case ErrorType.InvalidItem:
                    potentials = new[] {"What is that?", "I don't see that!"};
                    break;
                case ErrorType.InvalidLocation:
                    potentials = new[] {"Where?", "I can't go there!"};
                    break;
                case ErrorType.InvalidUse:
                    potentials = new[]
                                     {"How am I supposed to do that?", "I'm sorry Dave, I'm afraid I can't do that."};
                    break;
                case ErrorType.InvalidSingleUse:
                    potentials = new[] {"On what?"};
                    break;
                case ErrorType.General:
                default:
                    potentials = new[] {"Now you're just talking nonsense!", "What?"};
                    break;
            }
            Terminal.WriteLine(potentials[new Random().Next(potentials.Count())]);
        }

        #region Nested type: ErrorType

        private enum ErrorType
        {
            InvalidUse,
            InvalidLocation,
            General,
            InvalidItem,
            InvalidGrab,
            InvalidSingleUse
        }

        #endregion
    }
}