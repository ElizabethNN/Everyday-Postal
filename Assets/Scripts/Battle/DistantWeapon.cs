using UnityEngine;

namespace Battle
{
    public class DistantWeapon : MonoBehaviour, IWeapon
    {

        [SerializeField]
        private float attackSpeed;
        
        void Start()
        {
        
        }

        void Update()
        {
        
        }

        public void DoAttack(Vector3 targetPoint)
        {
            
        }

        public float AttackSpeed => attackSpeed;
        public int AttackDamage { get; }
        public float AttackRange { get; }
    }
}
