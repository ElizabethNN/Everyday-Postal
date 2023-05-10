using Battle;
using Pathfinding;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace AI
{
    public class Puppet : Playable
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
            if (_weapon.DoAttack(point))
            {
                OnAttackStart();
            }
        }

        public override int HealthPoint => healthPoint;
        public override float MoveSpeed => _movement == null ? GetComponent<AIPath>().maxSpeed : _movement.maxSpeed;
        public override int AttackDamage => _weapon?.AttackDamage ?? weapon.GetComponent<IWeapon>().AttackDamage;
        public override float AttackSpeed => _weapon?.AttackSpeed ?? weapon.GetComponent<IWeapon>().AttackSpeed;
        public override float AttackRange => _weapon?.AttackRange ?? weapon.GetComponent<IWeapon>().AttackRange;
        public override string Name => gameObject.name;
        public override void ReceiveDamage(int damage)
        {
            healthPoint -= damage;
            OnDamageReceived(healthPoint);
        }
    }
}
