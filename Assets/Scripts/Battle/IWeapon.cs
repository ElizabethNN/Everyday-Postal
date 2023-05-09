using UnityEngine;

namespace Battle
{
    public interface IWeapon
    {

        void DoAttack(Vector3 targetPoint);
        int AttackDamage { get; }
        float AttackSpeed { get; }
        float AttackRange { get; }

    }
}
