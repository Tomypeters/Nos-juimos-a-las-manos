using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    private List<Entity> enemies;
    
    // Awake
    protected override void Start()
    {
        base.Start();

        enemies = new List<Entity>();
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
        if (attack1 == 1)
        {
            attackStateMachine.AttemptTransition("Attack");
        }
        float attack2 = Input.GetAxis("Fire2");
        if (attack2 == 1)
        {
            // DASH
            attackStateMachine.AttemptTransition("Attack2");
        }

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
                    target = enemies[1];
                }
                else
                {
                    target = enemies[0];
                }
            }
            else if (target == null && enemies.Count > 0)
                target = enemies[0];
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemies.Add(collision.GetComponent<Entity>());
            if (target == null)
                target = collision.GetComponent<Entity>();
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            enemies.Remove(collision.GetComponent<Entity>());
    }

    protected override void Move()
    {
        rigidbody.velocity = new Vector2(moveDirection.x * moveSpeed,
            moveDirection.y * moveSpeed);

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

        if (animator != null)
        {
            animator.SetFloat("Horizontal", hands.transform.right.x);
            animator.SetFloat("Vertical", hands.transform.right.y);
        }
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

    private void OnAnimatorMove()
    {

    }
}
