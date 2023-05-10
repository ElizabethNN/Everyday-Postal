using UnityEngine;

namespace Battle
{
    public abstract class Playable : MonoBehaviour
    {
        public abstract int HealthPoint { get; }
        public abstract float MoveSpeed { get; }
        public abstract int AttackDamage { get; }
        public abstract float AttackSpeed { get; }
        public abstract float AttackRange { get; }
        public abstract string Name { get; }

        public abstract void ReceiveDamage(int damage);
        
        public event OnDamageCallback OnDamage;
        public event OnAttackCallback OnAttack;
        
        public delegate void OnDamageCallback(int damage);
        public delegate void OnAttackCallback();

        protected void OnAttackStart()
        {
            OnAttack?.Invoke();
        }

        protected void OnDamageReceived(int damage)
        {
            OnDamage?.Invoke(damage);
        }

    }
}
