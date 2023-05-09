using Generators.Entities;

namespace Generators
{
    public interface ILevelGenerationStep
    {
        public Room[,] DoGeneration(Room[,] rooms, float difficulty);
        
    }
}