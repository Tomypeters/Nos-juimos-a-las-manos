using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    private ScreenShake screenShake;
    private List<Entity> enemies;

    // Dash habilities
    public DashState dashState;
    public float maxDash = 0.2f;
    private float dashTimer;
    private bool dashKeyDown = false;

    private Vector2 savedVelocity;

    // Awake
    protected override void Start()
    {
        base.Start();

        enemies = new List<Entity>();
        screenShake = GetComponent<ScreenShake>();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        ProcessInputs();
    }


    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY);

        if (moveDirection != Vector2.zero && moveDirection.magnitude > 0.1)
            facingDirection = moveDirection.normalized;

        float attack1 = Input.GetAxis("Fire1");
        float attack2 = Input.GetAxis("Fire2");
        if (attackStateMachine.CurrentState == attackStateMachine.idleState)
        {
            
            if (attack2 == 1)
            {
                attackStateMachine.ChangeState(attackStateMachine.heavyAttackState);
            }
            else if (attack1 == 1)
            {
                attackStateMachine.ChangeState(attackStateMachine.attackState);
            }
        } 
       
        float dash = Input.GetAxis("Dash");
        if (dash == 1 && !dashKeyDown)
        {
            // DASH
            dashKeyDown = true;
        }

        else if (dashKeyDown)
            dashKeyDown = false;

        float targetSwitch = Input.GetAxis("Target");
        //bool switch1 = Input.GetButtonDown("joystick button 5");
        if (Input.anyKeyDown && (targetSwitch == 1 || targetSwitch == -1) )
        {
            if (target != null && enemies.Count > 1)
            {
                Vector2 targetVector = -(transform.position - target.transform.position);

                enemies.Sort(delegate (Entity a, Entity b)
                {
                    return ((int)Vector2.SignedAngle(targetVector, (transform.position - a.transform.position)) + 180) -
                        ((int)Vector2.SignedAngle(targetVector, (transform.position - b.transform.position)) + 180);
                    ;
                });

                if (targetSwitch == 1)
                {
                    enemies.Reverse();
                    UpdateTarget(enemies[1]);
                }
                else
                    UpdateTarget(enemies[0]);
                
            }
            else if (target == null && enemies.Count > 0)
                UpdateTarget(enemies[0]);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemies.Add(collision.GetComponent<Entity>());
            if (target == null)
                UpdateTarget(collision.GetComponent<Entity>());
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            enemies.Remove(collision.GetComponent<Entity>());
    }

    protected override void Move()
    {
        UpdateDash();

        if (dashState != DashState.Dashing)
        {

            rigidbody.velocity = new Vector2(moveDirection.x, moveDirection.y).normalized * moveSpeed;

            headAnimator.SetFloat("Velocity", rigidbody.velocity.magnitude);
            bodyAnimator.SetFloat("Velocity", rigidbody.velocity.magnitude);

        }

        if (target == null)
        {
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            hands.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Vector2 angleVector = target.transform.position - transform.position;
            float angle = Mathf.Atan2(angleVector.y, angleVector.x) * Mathf.Rad2Deg;
            hands.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        headAnimator.SetFloat("Horizontal", hands.transform.right.x);
        headAnimator.SetFloat("Vertical", hands.transform.right.y);
        bodyAnimator.SetFloat("Horizontal", hands.transform.right.x);
        bodyAnimator.SetFloat("Vertical", hands.transform.right.y);
    }

    public override void Attack()
    {
        base.Attack();
        hands.GetComponent<CircleCollider2D>().enabled = true;
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
        hands.GetComponent<CircleCollider2D>().enabled = false;
    }

    public override void TakeHit(float damage, Transform hitOrigin)
    {
        screenShake.shakeDuration = 0.1f;
        audioSystem.PlayTakeDamage();
        base.TakeHit(damage, hitOrigin);
    }

    private void OnAnimatorMove()
    {

    }
    public override void UpdateTarget(Entity target)
    {
        if (this.target != null)
            ((EnemyController)this.target).Deselect();
        base.UpdateTarget(target);
        ((EnemyController)target).Select();
    }


    void UpdateDash()
    {
        switch (dashState)
        {
            case DashState.Ready:
                if (dashKeyDown)
                {
                    savedVelocity = rigidbody.velocity;
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y).normalized * 8f;
                    dashState = DashState.Dashing;
                    // Set dash animation
                    //animator.SetFloat("Velocity", rigidbody.velocity.magnitude);
                    audioSystem.PlayDash();
                }
                break;
            case DashState.Dashing:
                dashTimer += Time.deltaTime * 3;
                if (dashTimer >= maxDash)
                {
                    dashTimer = maxDash;
                    rigidbody.velocity = savedVelocity;
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
        
    }

    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }
}
