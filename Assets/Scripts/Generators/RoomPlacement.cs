using System.Collections.Generic;
using Generators.Entities;
using Generators.Utils;

namespace Generators
{
    public class RoomPlacement : ILevelGenerationStep
    {
        private readonly List<string> _possibleRooms;

        public RoomPlacement(List<string> possibleRooms)
        {
            _possibleRooms = possibleRooms;
        }

        public Room[,] DoGeneration(Room[,] rooms, float difficulty)
        {
            var copy = new List<string>(_possibleRooms);
            for (int i = 0; i < rooms.GetLength(0); i++)
            {
                for (int j = 0; j < rooms.GetLength(1); j++)
                {
                    var randomIndex = RandomUtils.GetRandomInt(copy.Count);
                    rooms[i, j].Type = copy[randomIndex];

                    copy.RemoveAt(randomIndex);
                    if (copy.Count == 0)
                    {
                        copy = new List<string>(_possibleRooms);
                    }
                }
            }

            return rooms;
        }
    }
}