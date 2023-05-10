using UnityEngine;

namespace Battle
{
    public interface IWeapon
    {

        bool DoAttack(Vector3 targetPoint);
        int AttackDamage { get; }
        float AttackSpeed { get; }
        float AttackRange { get; }

    }
}
