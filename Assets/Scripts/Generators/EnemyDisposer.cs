using System.Collections.Generic;
using Battle;
using Generators.Entities;

namespace Generators
{
    public class EnemyDisposer : ILevelGenerationStep
    {
        private EnemySelector _selector;
        
        public EnemyDisposer(List<Playable> enemies, Playable hero)
        {
            _selector = new EnemySelector(enemies, hero);
        }
        
        public Room[,] DoGeneration(Room[,] rooms, float difficulty)
        {
            float perRoom = difficulty / (rooms.GetLength(0) * rooms.GetLength(1));
            float reminder = 0;
            for (int i = 0; i < rooms.GetLength(0); i++)
            {
                for (int j = 0; j < rooms.GetLength(1); j++)
                {
                    var level = perRoom + reminder;
                    var puppet = _selector[level];
                    while (puppet != null)
                    {
                        rooms[i, j].AddEnemy(puppet);
                        level -= _selector.GetDifficulty(puppet);
                        puppet = _selector[level];
                    }
                    reminder = level;
                }
            }

            return rooms;
        }
    }
}