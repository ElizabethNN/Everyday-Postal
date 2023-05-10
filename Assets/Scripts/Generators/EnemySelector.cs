using System.Collections.Generic;
using Battle;
using Generators.Utils;
using JetBrains.Annotations;

namespace Generators
{
    public class EnemySelector
    {
        private readonly List<Playable> _enemies;
        private readonly Playable _hero;

        public EnemySelector(List<Playable> enemies, Playable hero)
        {
            _enemies = enemies;
            _enemies.Sort((enemy1, enemy2) => GetDifficulty(enemy1).CompareTo(GetDifficulty(enemy2)));
            _hero = hero;
        }

        [CanBeNull]
        public Playable this[float difficulty]
        {
            get
            {
                var lastIndex = _enemies.FindLastIndex(enemy => GetDifficulty(enemy) <= difficulty); //formula
                if (lastIndex == -1)
                    return null;
                return _enemies[RandomUtils.GetRandomInt(lastIndex + 1)];
            }
        }

        public float GetDifficulty(Playable puppet)
        {
            var puppetRelativeHp = puppet.HealthPoint / (_hero.AttackDamage * _hero.AttackSpeed);
            var heroRelativeHp = (puppet.AttackDamage * puppet.AttackSpeed) / _hero.HealthPoint;
            return (puppetRelativeHp < 1 ? 1 : puppetRelativeHp) *
                   (heroRelativeHp > 1 ? 1 : heroRelativeHp) *
                   (puppet.MoveSpeed / _hero.MoveSpeed) * (puppet.AttackRange / _hero.AttackRange);
        }
    }
}