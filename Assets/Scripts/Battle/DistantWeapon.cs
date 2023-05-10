using UnityEngine;

namespace Battle
{
    public class DistantWeapon : MonoBehaviour, IWeapon
    {

        [SerializeField]
        private float attackSpeed;
        [SerializeField]
        private GameObject projectile;
        private float _timer;

        void FixedUpdate()
        {
            if (_timer > 0)
            {
                _timer -= Time.fixedDeltaTime;
            }
        }

        public bool DoAttack(Vector3 targetPoint)
        {
            if (_timer > 0)
            {
                return false;
            }

            _timer = 1 / attackSpeed;
            var position = transform.position;
            var dir = targetPoint - position.normalized;
            var projectile = Instantiate(this.projectile,
                new Vector3(position.x + dir.x * 0.1f, position.y + dir.y * 0.1f),
                Quaternion.FromToRotation(position, targetPoint));
            projectile.GetComponent<Projectile>().Init(targetPoint);
            return true;
        }

        public float AttackSpeed => attackSpeed;
        public int AttackDamage => projectile.GetComponent<Projectile>().Damage;
        public float AttackRange => projectile.GetComponent<Projectile>().Range;
    }
}
