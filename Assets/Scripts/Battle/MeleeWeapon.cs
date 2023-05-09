using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class MeleeWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField]
        private int attackDamage;

        [SerializeField] 
        private float attackSpeed;

        [SerializeField] 
        private float attackRange;

        [SerializeField]
        private float attackSector;
        
        private float _timer;
        private bool _shouldAttack;
        private Vector3 _targetPoint;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }

        private void FixedUpdate()
        {
            if (_shouldAttack && _timer <= 0)
            {
                _shouldAttack = false;
                var resultList = new List<RaycastHit2D>();
                var filter = new ContactFilter2D();
                filter.SetLayerMask(LayerMask.GetMask("Default", "Player"));
                var transformPosition = transform.position;
                Physics2D.CircleCast(transformPosition, attackRange, Vector3.up, filter, resultList);
                var target = _targetPoint - transformPosition;
                foreach (var hit in resultList)
                {
                    if(hit.collider.gameObject == transform.parent.gameObject)
                        continue;
                    var enemy = hit.transform.position - transformPosition;
                    if (Vector2.Angle(target, enemy) <= attackSector / 2)
                    {
                        GameObject o;
                        (o = hit.collider.gameObject).GetComponent<IPlayable>().ReceiveDamage(AttackDamage);
                        Debug.Log($"Dealt {attackDamage} damage to {o.name}");
                    }
                }
            }
            if (_timer > 0)
            {
                _timer -= Time.fixedDeltaTime;
            }
        }

        public void DoAttack(Vector3 targetPoint)
        {
            _targetPoint = targetPoint;
            if (_timer > 0)
            {
                return;
            }

            _shouldAttack = true;
            _timer = 1 / attackSpeed;
            
        }

        public int AttackDamage => attackDamage;
        public float AttackSpeed => attackSpeed;
        public float AttackRange => attackRange;
    }
}