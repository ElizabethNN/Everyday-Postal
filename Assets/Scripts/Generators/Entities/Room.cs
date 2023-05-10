using System.Collections.Generic;
using System.Collections.ObjectModel;
using Battle;
using Generators.Enums;

namespace Generators.Entities
{
    public class Room
    {
        private List<DirectionsEnum> _exits = new();
        private List<Playable> _enemies = new();
        public string Type { get; set; }

        public void AddExit(DirectionsEnum exit)
        {
            _exits.Add(exit);
        }

        public void AddEnemy(Playable enemy)
        {
            _enemies.Add(enemy);
        }

        public ReadOnlyCollection<DirectionsEnum> GetExits()
        {
            return _exits.AsReadOnly();
        }

        public ReadOnlyCollection<Playable> GetEnemies()
        {
            return _enemies.AsReadOnly();
        }

    }
}