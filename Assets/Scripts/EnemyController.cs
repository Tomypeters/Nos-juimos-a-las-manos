using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Entity
{
    public SpriteRenderer selectedSprite;
    
    private bool collided = false;
    private float collisionTimer = 0;
    private Vector3 collidedDirection = Vector3.up;


    // Awake
    protected override void Start()
    {
        base.Start();


    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Vector3 targetDir = (target.transform.position - transform.position);

        if (animator != null)
        {
            animator.SetFloat("Horizontal", targetDir.normalized.x);
            animator.SetFloat("Vertical", targetDir.normalized.y);
        }

        if (targetDir.magnitude >= attackRange)
        {
            if (collided)
            {
                transform.position += collidedDirection.normalized * moveSpeed/3 * Time.deltaTime;

            }
            else
            {
                transform.position += targetDir.normalized * moveSpeed * Time.deltaTime;
            }


        }
        else if (attackStateMachine.CurrentState == attackStateMachine.idleState)
        {
            attackStateMachine.ChangeState(attackStateMachine.thinkingState);
        }

        if (collisionTimer > 0)
            collisionTimer -= Time.deltaTime;

        if (collisionTimer <= 0)
            collided = false;

    }

    protected override void Move()
    {
        Vector2 angleVector = target.transform.position - transform.position;
        float angle = Mathf.Atan2(angleVector.y, angleVector.x) * Mathf.Rad2Deg;
        hands.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {

            float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
            float otherDistanceToTarget = Vector2.Distance(collision.collider.transform.position, target.transform.position);

            if (distanceToTarget > otherDistanceToTarget)
            {

                collided = true;
                collidedDirection = transform.position - collision.collider.transform.position;
                collisionTimer = 1f;
            }

        }
    }


    public override void TakeHit(float damage, Transform hitOrigin)
    {
        base.TakeHit(damage, hitOrigin);

        transform.position += (transform.position - hitOrigin.position).normalized * 0.5f;

    }

    public void Select()
    {
        selectedSprite.enabled = true;
    }
    public void Deselect()
    {
        selectedSprite.enabled = false;
    }


}
