using System;
using Battle;
using UnityEngine;

public class HeroMovement : MonoBehaviour, IPlayable
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
            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _weapon.DoAttack(new(worldPoint.x, worldPoint.y, 1));
        }
    }

    public int HealthPoint => health;
    public float MoveSpeed => speed;
    public int AttackDamage => _weapon?.AttackDamage ?? weapon.GetComponent<IWeapon>().AttackDamage;
    public float AttackSpeed => _weapon?.AttackSpeed ?? weapon.GetComponent<IWeapon>().AttackSpeed;
    public float AttackRange => _weapon?.AttackRange ?? weapon.GetComponent<IWeapon>().AttackRange;
    public string Name => gameObject.name;
    public void ReceiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
