using System;
using UnityEngine;

namespace Battle
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private int damage;
        [SerializeField] private float range;

        private Rigidbody2D _rigidbody2D;
        private Vector3 _startPoint;
        private Vector3 _velocity;
        private GameObject _ignore;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _startPoint = transform.position;
        }

        private void FixedUpdate()
        {
            var transform1 = transform;
            var position = transform1.position;
            position += _velocity * speed;
            transform1.position = position;
            if (Vector2.Distance(_startPoint, transform.position) >= range)
            {
                Destroy(gameObject);
            }
        }

        public void Init(Vector3 dir, GameObject parent)
        {
            _velocity = dir;
            _ignore = parent;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject != _ignore)
            {
                if (other.gameObject.TryGetComponent(typeof(Playable), out var playable))
                {
                    ((Playable)playable).ReceiveDamage(damage);
                }
                Destroy(gameObject);
            }
        }

        public int Damage => damage;
        public float Range => range;
    }
}