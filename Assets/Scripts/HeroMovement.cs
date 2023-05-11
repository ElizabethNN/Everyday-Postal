using System;
using Battle;
using UnityEngine;

public class HeroMovement : Playable
{
    [SerializeField]
    private float speed;

    [SerializeField] 
    private int health;
    
    private IWeapon _weapon;
    
    private Rigidbody2D _rigidbody2D;
    
    [SerializeField]
    private GameObject weapon;

    void Start()
    {
        _weapon = weapon.GetComponent<IWeapon>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal") * speed;
        var vertical = Input.GetAxis("Vertical") * speed;
        _rigidbody2D.velocity = new Vector2(horizontal, vertical);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            var worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
            if (_weapon.DoAttack(new(worldPoint.x, worldPoint.y, 0)))
            {
                OnAttackStart();
            }
        }
    }

    public override int HealthPoint => health;
    public override float MoveSpeed => speed;
    public override int AttackDamage => _weapon?.AttackDamage ?? weapon.GetComponent<IWeapon>().AttackDamage;
    public override float AttackSpeed => _weapon?.AttackSpeed ?? weapon.GetComponent<IWeapon>().AttackSpeed;
    public override float AttackRange => _weapon?.AttackRange ?? weapon.GetComponent<IWeapon>().AttackRange;
    public override string Name => gameObject.name;
    public override void ReceiveDamage(int damage)
    {
        health -= damage;
        OnDamageReceived(health);
    }
}
