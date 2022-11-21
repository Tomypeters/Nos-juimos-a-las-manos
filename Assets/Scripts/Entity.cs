
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // Movement
    public float moveSpeed;

    protected AudioSystem audioSystem;
    protected Rigidbody2D rigidbody;
    protected Animator animator;

    protected Vector2 moveDirection = Vector2.right;
    protected Vector2 facingDirection = Vector2.right;

    // Combat vars
    public Animator handAnimator;
    public GameObject hands;

    protected BaseStateMachine attackStateMachine;
    public float maxHealth = 5;
    public float health = 5;
    public float damage = 1;
    public float attackRange = 1.5f;
    public float attackCooldown = 0.5f;

    public Entity target;


    // Start is called before the first frame update
    protected virtual void Start()
    { 
        rigidbody = GetComponent<Rigidbody2D>();
        audioSystem = GetComponent<AudioSystem>();
        animator = GetComponent<Animator>();

        attackStateMachine = new BaseStateMachine();
        attackStateMachine.Init(this);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        attackStateMachine.Update();

    }

    void FixedUpdate()
    {
        // Physics calculations
        Move();
    }

    protected abstract void Move();

    public virtual void Attack()
    {
        handAnimator.SetTrigger("Attack");
        audioSystem.PlayWhoosh();
    }

    public virtual void FinishAttack()
    {
        //rigidbody.AddForce(moveDirection.normalized * 250f);
        if (target != null) {
            if (Vector2.Distance(transform.position, target.transform.position) <= attackRange)
            {
                target.TakeHit(damage, transform);
            }
        }
        //handAnimator.ResetTrigger("Attack");
    }


    public virtual void TakeHit(float damage, Transform hitOrigin)
    {
        audioSystem.PlayHitEnemy();
        health -= damage;
        if (health <= 0)
            Destroy(this.gameObject);
    }

    public virtual void UpdateTarget(Entity target)
    {
        this.target = target;
    }

}
