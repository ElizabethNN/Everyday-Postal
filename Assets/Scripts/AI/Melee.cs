using System;
using Unity.VisualScripting;
using UnityEngine;

namespace AI
{
    public class Melee : MonoBehaviour
    {

        private GameObject hero;
        private Puppet _puppet;
        private float maxChaseX;
        private float maxChaseY;
        private float minChaseX;
        private float minChaseY;
    
        void Start()
        {
            hero = GameObject.Find("Hero");
            _puppet = GetComponent<Puppet>();
            var position = transform.position;
            minChaseX = (int)MathF.Floor(position.x) / 20 * 20;
            minChaseY = ((int)MathF.Floor(position.y) / 20 - 1) * 20;
            maxChaseX = ((int)MathF.Floor(position.x) / 20 + 1) * 20;
            maxChaseY = (int)MathF.Floor(position.y) / 20 * 20;

        }

        private void FixedUpdate()
        {
            if (hero.IsDestroyed())
            {
                return;
            }

            _puppet.MoveToPoint(CheckSameRoom() ? hero.transform.position : transform.position);
            if (Vector2.Distance(transform.position, hero.transform.position) < _puppet.AttackRange / 2)
            {
                _puppet.Attack(hero.transform.position);
            }
        }

        private bool CheckSameRoom()
        {
            var position = hero.transform.position;
            return position.x > minChaseX &&
                   position.y > minChaseY &&
                   position.x < maxChaseX &&
                   position.y < maxChaseY;
        }
    }
}
