using System;
using Battle;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace AI
{
    public class Puppet : MonoBehaviour, IPlayable
    {
        private IWeapon _weapon;

        private AIPath _movement;

        [SerializeField]
        private int healthPoint;

        [SerializeField]
        private GameObject weapon;
        private void Awake()
        {
            _movement = GetComponent<AIPath>();
            _weapon = weapon.GetComponent<IWeapon>();
        }

        public void MoveToPoint(Vector2 point)
        {
            _movement.destination = point;
        }

        public void Attack(Vector2 point)
        {
            _weapon.DoAttack(point);
        }

        public int HealthPoint => healthPoint;
        public float MoveSpeed => _movement == null ? GetComponent<AIPath>().maxSpeed : _movement.maxSpeed;
        public int AttackDamage => _weapon?.AttackDamage ?? weapon.GetComponent<IWeapon>().AttackDamage;
        public float AttackSpeed => _weapon?.AttackSpeed ?? weapon.GetComponent<IWeapon>().AttackSpeed;
        public float AttackRange => _weapon?.AttackRange ?? weapon.GetComponent<IWeapon>().AttackRange;
        public string Name => gameObject.name;
        public void ReceiveDamage(int damage)
        {
            healthPoint -= damage;
            if (healthPoint <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
