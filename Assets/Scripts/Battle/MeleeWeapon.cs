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
        private string _layer;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }

        private void Start()
        {
            var ignored = transform.parent.gameObject.layer;
            var layers = new List<string> { "Default", "Player" };
            layers.Remove(LayerMask.LayerToName(ignored));
            _layer = layers[0];
        }

        private void FixedUpdate()
        {
            if (_shouldAttack && _timer <= 0)
            {
                _shouldAttack = false;
                var resultList = new List<RaycastHit2D>();
                var filter = new ContactFilter2D();
                filter.SetLayerMask(LayerMask.GetMask(_layer));
                var transformPosition = transform.position;
                Physics2D.CircleCast(transformPosition, attackRange, Vector3.up, filter, resultList);
                var target = _targetPoint - transformPosition;
                foreach (var hit in resultList)
                {
                    var enemy = hit.transform.position - transformPosition;
                    if (Vector2.Angle(target, enemy) <= attackSector / 2)
                    {
                        GameObject o;
                        (o = hit.collider.gameObject).GetComponent<Playable>().ReceiveDamage(AttackDamage);
                        Debug.Log($"Dealt {attackDamage} damage to {o.name}");
                    }
                }
            }
            if (_timer > 0)
            {
                _timer -= Time.fixedDeltaTime;
            }
        }

        public bool DoAttack(Vector3 targetPoint)
        {
            _targetPoint = targetPoint;
            if (_timer > 0 || _shouldAttack)
            {
                return false;
            }

            _shouldAttack = true;
            _timer = 1 / attackSpeed;
            return true;
        }

        public int AttackDamage => attackDamage;
        public float AttackSpeed => attackSpeed;
        public float AttackRange => attackRange;
    }
}