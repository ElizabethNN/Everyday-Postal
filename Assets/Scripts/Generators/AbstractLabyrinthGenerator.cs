using Generators.Entities;
using Generators.Enums;
using Generators.Utils;

namespace Generators
{
    public abstract class AbstractLabyrinthGenerator : ILevelGenerationStep
    {

        protected DirectionsEnum RandomMove()
        {
            return RandomUtils.GetRandomInt(0, 4) switch
            {
                0 => DirectionsEnum.Top,
                1 => DirectionsEnum.Right,
                2 => DirectionsEnum.Bottom,
                3 => DirectionsEnum.Left
            };
        }

        protected (int, int) RandomPoint(int xmax, int ymax)
        {
            return (RandomUtils.GetRandomInt(xmax), RandomUtils.GetRandomInt(ymax));
        }

        public abstract Room[,] DoGeneration(Room[,] labyrinth, float difficulty);
    }
}