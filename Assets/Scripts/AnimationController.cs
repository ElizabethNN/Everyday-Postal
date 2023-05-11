using Battle;
using Pathfinding;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    
    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private Animator animator;

    [SerializeField] 
    private new SpriteRenderer renderer;

    [SerializeField] 
    private AIPath aiPath;
    
    private Playable _playable;
    
    private bool _isaiPathNotNull;

    void Start()
    {
        _isaiPathNotNull = aiPath != null;
        _playable = GetComponent<Playable>();
        _playable.OnDamage += OnDamageReceived;
        _playable.OnAttack += OnAttack;
    }

    private void FixedUpdate()
    {
        var currentAnimation = animator.GetCurrentAnimatorStateInfo(0);
        if (currentAnimation.IsName("Attack") || currentAnimation.IsName("Death"))
        {
            return;
        }

        if (rigidBody.velocity.y > 0 || (_isaiPathNotNull && aiPath.velocity.y > 0))
        {
            animator.Play("MoveBack");
        }
        else if(rigidBody.velocity is { x: 0, y: 0 } || (_isaiPathNotNull && aiPath.velocity is { x: 0, y: 0 }))
        {
            animator.Play("Idle");
        }
        else
        {
            renderer.flipX = rigidBody.velocity.x > 0;
            animator.Play("Move");
        }

    }

    private void OnDamageReceived(int health)
    {
        if (health > 0)
        {
            return;
        }
        animator.Play("Death");
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnAttack()
    {
        animator.Play("Attack");
    }
}
