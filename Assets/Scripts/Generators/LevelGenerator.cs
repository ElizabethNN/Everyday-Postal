using System.Collections.Generic;
using Generators.Entities;

namespace Generators
{
    public class LevelGenerator
    {

        private List<ILevelGenerationStep> _steps;

        public LevelGenerator(List<ILevelGenerationStep> steps)
        {
            _steps = steps;
        }

        public Room[,] Generate(int x, int y, float difficulty)
        {
            Room[,] room = new Room[x,y];
            foreach (var step in _steps)
            {
                room = step.DoGeneration(room, difficulty);
            }

            return room;
        }
    }
}